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

            return View(posts);
        }

        public ActionResult Show(Guid id)
        {
            var post = DataSession.Load<Post>(id);
            return View(post);
        }

        [CustomAuthorize]
        public ActionResult New()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult Edit(Guid id)
        {
            var post = DataSession.Load<Post>(id);
            return View(post);
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult New(Post post)
        {
            if (post.Title != null && post.Title.Length>0 && post.Content!=null && post.Content.Length>0)
            {
                post.Published = DateTime.Now;
                post.Created = DateTime.Now;
                DataSession.SaveOrUpdate(post);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Some fields are invalid.");
                return View(post);
            }
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult Edit(Guid id, Post post)
        {
            //get teh original and update it
            Post original = DataSession.Load<Post>(id);
            if (post.Title != null && post.Title.Length > 0 && post.Content != null && post.Content.Length > 0)
            {
                original.Hash = post.Hash;
                original.Title = post.Title;
                original.Content = post.Content;

                DataSession.SaveOrUpdate(original);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Some fields are invalid.");
                return View(post);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
