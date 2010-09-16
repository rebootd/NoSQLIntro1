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
		public void can_fetch_by_title_index()
		{
			can_fetch_by_index("PostsByTitle");
		}

		[Fact]
		public void can_fetch_by_hash_index()
		{
			can_fetch_by_index("PostsByHash");
		}

        [Fact]
        public void can_fetch_by_tag_index()
        {
            can_fetch_by_index("PostsByTag");
        }

        [Fact]
        public void can_fetch_by_pub_index()
        {
            can_fetch_by_index("PostsByPublished");
        }

		private void can_fetch_by_index(string index)
		{
            var posts = fetch_by_index<Post>(index);
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
            int count = 1000;
            DateTime start = DateTime.Now;
            var s = DocumentSession;
            for (int loop = 1; loop <= count; loop++)
            {
                Post post = new Post
                {
                    Content = "test content",
                    Hash = "perf-test",
                    Title = "perf test",
                    Created = DateTime.Now,
                    Published = DateTime.Now.AddYears(10)
                };
                if (post.Tags == null) post.Tags = new List<Tag>();
                post.Tags.Add(new Tag { Name = "perf" });                
                s.Store(post);

                if (loop % 30 == 0)
                {
                    s.SaveChanges();
                    s = DocumentSession;
                }
            }

            s.SaveChanges();

            TimeSpan span = DateTime.Now - start;
            System.Diagnostics.Debug.WriteLine("raven insert span: " + span.TotalMilliseconds.ToString());
            System.Diagnostics.Debug.WriteLine("rows/sec: " + (count / span.TotalSeconds).ToString());

            clean_up_test_data();
        }

        [Fact]
        public void clean_up_test_data()
        {
            //now clean up added records
            var s = DocumentSession;
            var posts = s.LuceneQuery<Post>("PostsByTitle")
                //.WaitForNonStaleResults(TimeSpan.FromSeconds(5))
                .Where(x => x.Title == "perf test")
                .ToList();

            foreach (Post post in posts)
            {
                s.Delete<Post>(post);
            }
            s.SaveChanges();
        }
    }
}