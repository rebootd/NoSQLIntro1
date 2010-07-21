using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSQL.Models;

namespace BlogSQL.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            //var post = new Post()
            //{
            //    Hash = "test-post",
            //    Title = "test post",
            //    Content = "test post content",
            //    Published = DateTime.Now.AddDays(-7),
            //    Created = DateTime.Now
            //};
            //DataSession.SaveOrUpdate(post);

            var posts = DataSession.CreateCriteria<Post>().List<Post>();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
