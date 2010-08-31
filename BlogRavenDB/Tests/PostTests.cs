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
    }
}