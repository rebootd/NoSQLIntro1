using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                var collPosts = db.GetCollection<Tag>();
                var col = collPosts.Find();
                if (col != null)
                    tags = col.ToList();
            }
        }

        [Fact]
        public void fecth_unique()
        {
            List<Tag> tags = new List<Tag>();
            using (var db = Mongo.Create(ConnectionString()))
            {
                var collPosts = db.GetCollection<Tag>();
                var col = collPosts.Find();
                if (col != null)
                    tags = col.ToList();

                var uniqueTags = from t in tags
                                 group t by t.Name into g
                                 select new { SetKey = g.Key, Count = g.Count() };

                Assert.True(uniqueTags != null);
            }
        }
    }
}