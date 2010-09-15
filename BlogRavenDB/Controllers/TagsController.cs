using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogRavenDB.Models;

namespace BlogRavenDB.Controllers
{
    public class TagsController : BaseController
    {
        public ActionResult Show(string name)
        {
            var posts = (from p in DocumentSession.LuceneQuery<Post>("PostsByPublished")
                         where p.Published <= DateTime.Now
                         && p.Tags != null
                         && p.Tags.Any(x => x.Name == name)
                         select p)
                      .ToList();

            return View(posts);
        }

    }
}
