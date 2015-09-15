using System;
using System.IO;
using System.Web.Mvc;
using CollageMaker.Models;
using Tweetinvi;

namespace CollageMaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGettingFileAndImage _helperFileAndImage;
        public HomeController() : this(new FileAndImageHelper()) { }
        public HomeController(IGettingFileAndImage helperFileAndImage)
        {
            _helperFileAndImage = helperFileAndImage;
        }
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            Auth.SetUserCredentials("Access_Token", "Access_Token_Secret", "Consumer_Key", "Consumer_Secret");
            DataModel model = new DataModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(DataModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var users = Tweetinvi.User.GetUserFromScreenName(model.UserName);
                    var friends = users.GetFriends();
                    _helperFileAndImage.GetFileFromTwitter(friends);
                    ViewBag.NameUser = model.UserName;
                    _helperFileAndImage.CreateCollage(model.NumberOfColumns, model.NumberOfRows);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMsg = e.Message;
                    return View("Error");
                }
                ViewBag.UserName = model.UserName;
                return View("Result");
            }
            return View();
        }
        public ActionResult Download()
        {
            var dir = Server.MapPath("/Content/DoneCollage/");
            var path = Path.Combine(dir, "result.jpg");
            return File(path, "image/jpeg", "result.jpg");
        }
    }
}