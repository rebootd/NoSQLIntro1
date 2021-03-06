﻿using System;
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
            var collPosts = CurrentMongoSession.GetCollection<Post>();
            var col = collPosts.Find(new { Published = Q.LessOrEqual(DateTime.Now) }).Take(5);
            var posts = col.ToList();

            return View(posts);
        }

        public ActionResult Show(Guid id)
        {   
            var collPosts = CurrentMongoSession.GetCollection<Post>();
            Post post = collPosts.FindOne(new { Id = id });
            return View(post);
        }

        public ActionResult ShowHashed(int year, int month, string hash)
        {
            Post post = null;
            var collPosts = CurrentMongoSession.GetCollection<Post>();
            var pq = collPosts.Find(new { Hash = hash })
                .Where(p => p.Published.Year == year && p.Published.Month == month);
            if (pq.Count()>0)
                post = pq.ToList()[0];
            return View("Show", post);
            //should probably handle null (post not found) here
        }

        [CustomAuthorize]
        public ActionResult New()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult Edit(Guid id)
        {   
            var collPosts = CurrentMongoSession.GetCollection<Post>();
            Post post = collPosts.FindOne(new { Id = id });
            return View(post);
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult New(Post post)
        {
            if (post.Title != null && post.Title.Length > 0 && post.Content != null && post.Content.Length > 0)
            {
                post.Id = Guid.NewGuid();
                post.Published = DateTime.Now;
                post.Created = DateTime.Now;
				//update tags
				string taglist = Request.Form["Tags"];
				string[] tags = taglist.Split(',');

				foreach (string tag in tags)
					if (tag != null && tag.Length > 0) post.Tags.Add(new Tag { Name = tag });

				var collPosts = CurrentMongoSession.GetCollection<Post>();
                collPosts.Insert(post);

                return RedirectToAction("Index");
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
            var collPosts = CurrentMongoSession.GetCollection<Post>();
            Post original = collPosts.FindOne(new { Id = id });

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

                collPosts.Save(original);
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
