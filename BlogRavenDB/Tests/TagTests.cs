using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogRavenDB.Models;
using Xunit;
using Raven.Client.Document;

namespace BlogRavenDB.Tests
{
    public class TagTests : TestBase
    {
        [Fact]  
        public void can_fetch()
        {
            var tags = DocumentSession.LuceneQuery<Tag>("TagsByName").WaitForNonStaleResults().ToList();
            Assert.NotEmpty(tags);
        }

        [Fact]
        public void fecth_unique()
        {
			var alltags = DocumentSession.LuceneQuery<Tag>("TagsByName").WaitForNonStaleResults().ToList();
			List<string> tags = (from t in alltags
								 select t.Name)
								.ToList();

            Assert.NotEmpty(tags);
        }

        [Fact]
        public void find_posts_by_tag()
        {
            string name = "yours";

			var posts = DocumentSession.LuceneQuery<Post>("PostsByTag")
				.Where(x => x.Published <= DateTime.Now && x.Tags.Any(y => y.Name == name))
				.ToList();
            
            Assert.NotEmpty(posts);
        }
    }
}