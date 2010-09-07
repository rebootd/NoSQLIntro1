using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogMongoDB.Models;
using Norm;
using Xunit;

namespace BlogMongoDB.Tests
{
    public class PostTests : TestBase
    {
        [Fact]
        public void can_fetch()
        {
            List<Post> posts = new List<Post>();
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();
                var col = collPosts.Find(new { Published = Q.LessOrEqual(DateTime.Now) });
                if (col != null)
                {
                    posts = col.ToList();
                }
            }
        }

        [Fact]
        public void do_perf_inserts()
        {
            int count = 100000;
            DateTime start = DateTime.Now;
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Post>();

                for (int loop = 1; loop <= count; loop++)
                {
                    Post post = new Post
                    {
                        Id = Guid.NewGuid(),
                        Content = "test content",
                        Hash = "perf-test",
                        Title = "perf test",
                        Created = DateTime.Now,
                        Published = DateTime.Now.AddYears(1000)
                    };
                    if (post.Tags == null) post.Tags = new List<Tag>();
                    post.Tags.Add(new Tag { Name = "perf" });

                    collPosts.Insert(post);
                }            
            }
            TimeSpan span = DateTime.Now - start;
            System.Diagnostics.Debug.WriteLine("mongo insert span: " + span.TotalMilliseconds.ToString());
            System.Diagnostics.Debug.WriteLine("rows/sec: " + (count / span.TotalSeconds).ToString());
        }
    }
}