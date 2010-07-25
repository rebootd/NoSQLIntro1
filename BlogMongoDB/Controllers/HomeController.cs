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
            //ViewData["Message"] = "Welcome to ASP.NET MVC!";

            //var post = new Post()
            //{
            //    Hash = "test-post",
            //    Title = "test post",
            //    Content = "test post content",
            //    Published = DateTime.Now.AddDays(-1),
            //    Created = DateTime.Now
            //};

            List<Post> posts = new List<Post>();
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                //collPosts.Insert(post);
                var col = collPosts.Find();
                if (col != null)
                    posts = col.ToList();
            }

            return View(posts);
        }

        public ActionResult Show(Guid id)
        {
            //var post = DataSession.Load<Post>(id);
            //return View(post);
            return View();
        }

        [CustomAuthorize]
        public ActionResult New()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult Edit(Guid id)
        {
            //var post = DataSession.Load<Post>(id);
            //return View(post);
            return View();
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult New(Post post)
        {
            //if (post.Title != null && post.Title.Length > 0 && post.Content != null && post.Content.Length > 0)
            //{
            //    post.Published = DateTime.Now;
            //    post.Created = DateTime.Now;
            //    DataSession.SaveOrUpdate(post);
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Some fields are invalid.");
            //    return View(post);
            //}
            return View();
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult Edit(Guid id, Post post)
        {
            //get teh original and update it
            //Post original = DataSession.Load<Post>(id);
            //if (post.Title != null && post.Title.Length > 0 && post.Content != null && post.Content.Length > 0)
            //{
            //    original.Hash = post.Hash;
            //    original.Title = post.Title;
            //    original.Content = post.Content;

            //    DataSession.SaveOrUpdate(original);
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Some fields are invalid.");
            //    return View(post);
            //}
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
