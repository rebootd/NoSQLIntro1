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
            var tagsQuery = DocumentSession.Query<Tag>("TagsByName");
            List<Tag> tags = new List<Tag>();
            if (tagsQuery != null)
            {
                tags = tagsQuery.ToList();
            }
            Assert.NotEmpty(tags);
        }

        [Fact]
        public void fecth_unique()
        {
            List<string> tags = new List<string>();
            var uniqueTags = from t in DocumentSession.LuceneQuery<Tag>("TagsByName")
                             group t by t.Name into g
                             select new { SetKey = g.Key, Count = g.Count() };

            foreach (var entry in uniqueTags.ToList())
                tags.Add(entry.SetKey.ToString());

            Assert.NotEmpty(tags);
        }

        [Fact]
        public void find_posts_by_tag()
        {
            string name = "yours";

            List<Post> posts = new List<Post>();
            var ps = (from p in DocumentSession.LuceneQuery<Post>("PostsByPublished")
                      where p.Published <= DateTime.Now
                      && p.Tags != null
                      && p.Tags.Any(x => x.Name == name)
                      select p);
            if (ps != null)
                posts = ps.ToList();

            Assert.NotEmpty(posts);
        }
    }
}