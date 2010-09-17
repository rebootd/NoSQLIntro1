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
    }
}