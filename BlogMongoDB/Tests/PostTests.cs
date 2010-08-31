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
                var col = collPosts.Find();
                if (col != null)
                    posts = col.ToList();
            }
        }
    }
}