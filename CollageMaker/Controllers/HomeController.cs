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
            Auth.SetUserCredentials("8NpXC8v84DRas4qsn2KCBMQRb",
                    "ckND9vF3Kr8KBJ3BQXVn9zzEXCZCefMYAGIqtNScUijoFcAgKH",
                    "936315930-WJlbXnyvqlJYx5j5s6U5TI3OYtqbQbnF07eFqnm3",
                    "GuQVe3cykwOPPk8bXEvGPghjBKfUvkP7O7VWXgFP6MKP1");
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