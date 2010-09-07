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
            List<Post> posts = new List<Post>();
            var ps = (from p in DocumentSession.LuceneQuery<Post>("PostsByPublished")
                      where p.Published <= DateTime.Now
                      && p.Tags != null
                      && p.Tags.Any(x => x.Name == name)
                      select p);
            if (ps != null)
                posts = ps.ToList();

            return View(posts);
        }

    }
}
