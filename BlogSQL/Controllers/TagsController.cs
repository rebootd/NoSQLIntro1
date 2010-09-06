using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSQL.Models;
using NHibernate.Criterion;

namespace BlogSQL.Controllers
{
    public class TagsController : BaseController
    {
        //show posts for a selected tag
        public ActionResult Show(string name)
        {
            IList<Tag> tags = DataSession.CreateCriteria(typeof(Tag))
                    .Add(Restrictions.Eq("Name", name))
                    .List<Tag>();

            List<Post> posts = new List<Post>();
            foreach (Tag tag in tags)
                posts.Add(tag.Post);
                
            return View(posts);
        }

    }
}
