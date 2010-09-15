using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMongoDB.Models;
using Norm;

namespace BlogMongoDB.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        public ActionResult TagCloud()
        {
            return View("TagCloud", GetUniqueTags());
        }

        public List<string> GetUniqueTags()
        {
			string map = "function(){ this.Tags.forEach(function(z){ emit( z , { count : 1 } ); } ); };";
			string reduce = "function(key,values){ return 1; };";
			var mr = CurrentMongoSession.Database.CreateMapReduce();
			var query = new { Published = Q.LessOrEqual(DateTime.Now) };
			var response = mr.Execute(new MapReduceOptions<Post> { Map = map, Reduce = reduce, Query = query });
			var alltags = response.GetCollection<TagReduce>().Find().ToList();
			
			return (from t in alltags
						  select t.Id.Name)
						 .ToList<string>();
        }

		public Mongo CurrentMongoSession
		{
			get{ return MvcApplication.CurrentSession; }
		}

        public bool IsLoggedIn
        {
            get 
            {
                return (CurrentAuthor == null);
            }
        }

        public Author CurrentAuthor
        {
            get
            {
                return Session["author"] as Author;
            }
            protected set
            {
                Session["author"] = value;
            }
        }

        protected void LogoffAuthor()
        {
            Session.Clear();
        }        
    }
}
