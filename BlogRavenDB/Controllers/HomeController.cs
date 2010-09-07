using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogRavenDB.Models;

namespace BlogRavenDB.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<Post> posts = new List<Post>();
            var postsQuery = DocumentSession.LuceneQuery<Post>("PostsByPublished")
                .Where(p => p.Published <= DateTime.Now);

            if (postsQuery != null)
            {
                posts = postsQuery.ToList();
            }

            return View(posts);
        }

        public ActionResult Show(string id)
        {
            var post = DocumentSession.Load<Post>(id);
            return View(post);
        }

        [CustomAuthorize]
        public ActionResult New()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult Edit(string id)
        {
            var post = DocumentSession.Load<Post>(id);
            return View(post);
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult New(Post post)
        {
            if (post.Title != null && post.Title.Length > 0 && post.Content != null && post.Content.Length > 0)
            {
                post.Published = DateTime.Now;
                post.Created = DateTime.Now;
				//update tags
				string taglist = Request.Form["Tags"];
				string[] tags = taglist.Split(',');

				foreach (string tag in tags)
					if (tag != null && tag.Length > 0) post.Tags.Add(new Tag { Name = tag });

                DocumentSession.Store(post);
                DocumentSession.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Some fields are invalid.");
                return View(post);
            }
            //return View();
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult Edit(string id, Post post)
        {
            //get teh original and update it
            Post original = DocumentSession.Load<Post>(id);
            if (post.Title != null && post.Title.Length > 0 && post.Content != null && post.Content.Length > 0)
            {
                original.Hash = post.Hash;
                original.Title = post.Title;
                original.Content = post.Content;
				//update tags
				string taglist = Request.Form["Tags"];
				string[] tags = taglist.Split(',');
				original.Tags.Clear();
				foreach (string tag in tags)
					if (tag != null && tag.Length > 0) original.Tags.Add(new Tag { Name = tag });

                DocumentSession.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Some fields are invalid.");
                return View(post);
            }
            //return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
