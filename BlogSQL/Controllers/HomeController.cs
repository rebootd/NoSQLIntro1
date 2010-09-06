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
				//update tags
				string taglist = Request.Form["Tags"];
				string[] tags = taglist.Split(',');
				foreach (string tag in tags)
					post.Tags.Add(new Tag { Name = tag, Post = post });

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
				ClearOldTags(original);
				//update tags
				string taglist = Request.Form["Tags"];
				string[] tags = taglist.Split(',');
				
				foreach(string tag in tags)
					if(tag!=null && tag.Length>0) original.Tags.Add(new Tag {Name = tag, Post = original });

                DataSession.SaveOrUpdate(original);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Some fields are invalid.");
                return View(post);
            }
        }

		private void ClearOldTags(Post post)
		{
			foreach (Tag tag in post.Tags)
			{
				DataSession.Delete(tag);
			}
			post.Tags.Clear(); //remove old tags
		}

        public ActionResult About()
        {
            return View();
        }
    }
}
