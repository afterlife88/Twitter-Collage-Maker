using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Collage.Engine;
using Tweetinvi.Core.Interfaces;

namespace CollageMaker.Models
{
    public class FileAndImageHelper : IGettingFileAndImage
    {
        private readonly Regex _illegalInFileName = new Regex(@"[\\/:*?""<>|]");
        public void GetFileFromTwitter(IEnumerable<IUser> friends)
        {
            using (var client = new WebClient())
            {
                foreach (var get in friends)
                {
                    var mystring = _illegalInFileName.Replace(get.ProfileImageUrl, "");
                    client.DownloadFile(new Uri(get.ProfileImageUrl),
                        System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/") + mystring);
                }
            }
        }
        public void CreateCollage(int columns, int rows)
        {
            DirectoryInfo di = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/"));
            DirectoryInfo diroutput = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/Content/DoneCollage/"));
            {
                List<FileInfo> files = di.GetFiles().ToList();
                var setting = new CollageSettings(
                    new CollageDimensionSettings
                    {
                        NumberOfColumns = columns,
                        NumberOfRows = rows,
                        TileScalePercent = new Percentage(100),
                        TileHeight = 50,
                        TileWidth = 50
                    },
                    new AdditionalCollageSettings
                    {
                        CutTileRandomly = false,
                        RotateAndFlipRandomly = false
                    },
                    files, diroutput);
                var collage = new CollageGenerator(setting);
                collage.Create();
                if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Content/DoneCollage/") + "result.jpg"))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                }
            }
        }
    }
}
