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
            
            var collPosts = CurrentMongoSession.GetCollection<Post>();
            var col = collPosts.Find(new { Published = Q.LessOrEqual(DateTime.Now) });
            if (col != null)
            {
                posts = col.ToList();
            }
        }        
    }
}