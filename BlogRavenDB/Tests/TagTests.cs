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
        }

        [Fact]
        public void fecth_unique()
        {
            var uniqueTags = from t in DocumentSession.Query<Tag>("TagsByName")
                             group t by t.Name into g
                             select new { SetKey = g.Key, Count = g.Count() };

            Assert.True(uniqueTags != null);
        }
    }
}