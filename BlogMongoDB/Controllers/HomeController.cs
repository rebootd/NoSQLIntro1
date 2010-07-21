using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMongoDB.Models;
using Norm;

namespace BlogMongoDB.Controllers
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
            //    Published = DateTime.Now.AddDays(-1),
            //    Created = DateTime.Now
            //};

            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                //collPosts.Insert(post);
                var col = collPosts.Find();
                int count = col == null ? 0 : col.Count();
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [HttpPut]
        public ActionResult Update(Post post)
        {
            return View();
        }
    }
}
