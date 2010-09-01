using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogRavenDB.Models;
using Xunit;
using Raven.Client.Document;

namespace BlogRavenDB.Tests
{
    public class PostTests : TestBase
    {
        [Fact]  
        public void can_fetch()
        {
            var postsQuery = DocumentSession.Query<Post>("PostsByPublished");
            List<Post> posts = new List<Post>();
            if (postsQuery != null)
            {
                posts = postsQuery.ToList();
            }
        }

        [Fact]
        public void do_perf_inserts()
        {
            int count = 100000;
            DateTime start = DateTime.Now;
            var session = DocumentSession;
            for (int loop = 1; loop <= count; loop++)
            {
                Post post = new Post
                {
                    Content = "test content",
                    Hash = "perf-test",
                    Title = "perf test",
                    Created = DateTime.Now,
                    Published = DateTime.Now.AddYears(1000)
                };
                if (post.Tags == null) post.Tags = new List<Tag>();
                post.Tags.Add(new Tag { Name = "perf" });
                session.Store(post);
            }
            DocumentSession.SaveChanges();
            TimeSpan span = DateTime.Now - start;
            System.Diagnostics.Debug.WriteLine("raven insert span: " + span.TotalMilliseconds.ToString());
            System.Diagnostics.Debug.WriteLine("rows/sec: " + (count / span.TotalSeconds).ToString());
        }
    }
}