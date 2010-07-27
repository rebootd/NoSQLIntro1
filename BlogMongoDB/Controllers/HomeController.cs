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
        //hackery..
        //public ActionResult AddPost()
        //{
        //    var post = new Post()
        //    {
        //        Hash = "test-post",
        //        Title = "test post",
        //        Content = "test post content",
        //        Published = DateTime.Now.AddDays(-1),
        //        Created = DateTime.Now
        //    };

        //    using (var db = Mongo.Create(ConnectionString()))
        //    {
        //        var collPosts = db.GetCollection<Post>();
        //        collPosts.Insert(post);
        //    }
        //    return RedirectToAction("Index");
        //}

        public ActionResult Index()
        {
            List<Post> posts = new List<Post>();
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                var col = collPosts.Find();
                if (col != null)
                    posts = col.ToList();
            }

            return View(posts);
        }

        public ActionResult Show(Guid id)
        {
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                Post post = collPosts.FindOne(new { Id = id });
                return View(post);
            }
        }

        [CustomAuthorize]
        public ActionResult New()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult Edit(Guid id)
        {
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                Post post = collPosts.FindOne(new { Id = id });
                return View(post);
            }
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
                using (var db = Mongo.Create(ConnectionString()))
                {
                    var collPosts = db.GetCollection<Post>();
                    collPosts.Insert(post);
                }
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
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                Post original = collPosts.FindOne(new { Id = id });

                if (post.Title != null && post.Title.Length > 0 && post.Content != null && post.Content.Length > 0)
                {
                    original.Hash = post.Hash;
                    original.Title = post.Title;
                    original.Content = post.Content;

                    collPosts.Save(original);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Some fields are invalid.");
                    return View(post);
                }
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
