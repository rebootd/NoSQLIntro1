using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using BlogMongoDB.Models;
using Norm;
using Xunit;

namespace BlogMongoDB.Tests
{
    public class TagTests : TestBase
    {
        [Fact]
        public void can_fetch()
        {
            List<Tag> tags = new List<Tag>();
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                var posts = collPosts.Find(new { Published = Q.LessOrEqual(DateTime.Now) }).ToList();
                var ts = from p in posts
                           from t in p.Tags
                           where p.Tags != null
                           select t;
                tags = ts.ToList<Tag>();
            }
            Assert.NotEmpty(tags);
        }

        [Fact]
        public void fecth_unique()
        {
            List<string> tags = new List<string>();
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                var posts = collPosts.Find(new { Published = Q.LessOrEqual(DateTime.Now) }).ToList();
                var ts = from p in posts
                         from t in p.Tags
                         where p.Tags != null
                         select t;
                List<Tag> alltags = ts.ToList<Tag>();

                var uniqueTags = from t in alltags
                                 group t by t.Name into g
                                 select new { SetKey = g.Key, Count = g.Count() };
                foreach (var entry in uniqueTags)
                {
                    tags.Add(entry.SetKey.ToString());
                }
            }

            Assert.NotEmpty(tags);
        }

        [Fact]
        public void find_posts_by_tag()
        {
            string name = "Yours";

            List<Post> posts = new List<Post>();
            using (var db = Mongo.Create(ConnectionString()))
            {
                var col = db.GetCollection<Post>();
                var postCol = col.Find();
                var matches = from p in postCol
                              where p.Tags != null
                              && p.Tags.Any(t => t.Name == name)
                              select p;

                foreach (var entry in matches)
                {
                    posts.Add((Post)entry);
                }
            }
            
            //foreach (Tag tag in tags)
            //    posts.Add(tag.Post);

            Assert.NotEmpty(posts);
        }
    }
}