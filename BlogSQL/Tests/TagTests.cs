using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSQL.Models;
using NHibernate;
using NHibernate.Criterion;
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

        [Fact]
        public void fecth_unique()
        {
            var uniqueTags = from t in DataSession.CreateCriteria<Tag>().List<Tag>()
                             group t by t.Name into g
                             select new { SetKey = g.Key, Count = g.Count() };

            //foreach (var entry in uniqueTags)
            //{
            //    var e = entry;
            //}
            //Assert.NotEmpty(uniqueTags);
        }
    }
}