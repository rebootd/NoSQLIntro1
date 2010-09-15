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
			List<Post> posts = DocumentSession.LuceneQuery<Post>("PostsByPublished")
				.WaitForNonStaleResults()
				.ToList();

            Assert.NotEmpty(posts);
        }

		[Fact]
		public void can_fetch_by_title()
		{
			can_fetch_by_index("PostsByTitle");
		}

		[Fact]
		public void can_fetch_by_hash()
		{
			can_fetch_by_index("PostsByHash");
		}

		private void can_fetch_by_index(string index)
		{
			var posts = DocumentSession.LuceneQuery<Post>(index)
				.ToList();
			Assert.NotEmpty(posts);
		}

        [Fact]
        public void can_fetch_by_tag()
        {
            string name = "yours";

            var posts = DocumentSession.LuceneQuery<Post>("PostsByTag")
				.Where(x => x.Published <= DateTime.Now && x.Tags.Any(y => y.Name == name))
				.ToList();            

            Assert.NotEmpty(posts);
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
            
			session.SaveChanges();
            TimeSpan span = DateTime.Now - start;
            System.Diagnostics.Debug.WriteLine("raven insert span: " + span.TotalMilliseconds.ToString());
            System.Diagnostics.Debug.WriteLine("rows/sec: " + (count / span.TotalSeconds).ToString());

			//now clean up added records
			var posts = session.LuceneQuery<Post>("PostsByHash")
				.WaitForNonStaleResults()
				.Where(x => x.Hash == "perf-test")
				.ToList();
			foreach(Post post in posts)
				session.Delete<Post>(post);
			session.SaveChanges();
        }
    }
}