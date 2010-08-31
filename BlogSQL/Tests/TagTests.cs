using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSQL.Models;
using Xunit;

namespace BlogSQL.Tests
{
    public class TagTests : TestBase
    {
        [Fact]
        public void can_fetch()
        {
            var tags = DataSession.CreateCriteria<Tag>().List<Tag>();
        }
    }
}