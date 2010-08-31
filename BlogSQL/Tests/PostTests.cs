using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSQL.Models;
using Xunit;

namespace BlogSQL.Tests
{
    public class PostTests : TestBase
    {
        [Fact]
        public void can_fetch()
        {
            var posts = DataSession.CreateCriteria<Post>().List<Post>();
        }
    }
}