using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMongoDB.Models;
using Norm;

namespace BlogMongoDB.Controllers
{
    public class TagsController : BaseController
    {
        //
        // GET: /Tags/

        public ActionResult Show(string name)
        {
            List<Post> posts = new List<Post>();

			var collPosts = CurrentMongoSession.GetCollection<Post>();
            var postquery = collPosts.Find(new { Published = Q.LessOrEqual(DateTime.Now) }).ToList();
            var ts = from p in postquery
                        where p.Tags != null
                        && p.Tags.Any(x => x.Name == name)
                        select p;
            posts = ts.ToList<Post>();

            return View(posts);
        }

    }
}
