using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace CollageMaker.Models
{
    public interface IGettingFileAndImage
    {
        void GetFileFromTwitter(IEnumerable<IUser> friends);
        void CreateCollage(int columns, int rows);
    }
}
