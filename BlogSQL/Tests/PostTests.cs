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

        [Fact]
        public void do_perf_inserts()
        {
            int count = 100000;
            DateTime start = DateTime.Now;
            for (int loop = 1; loop <= count; loop++)
            {
                Post post = new Post { Content = "test content", Hash = "perf-test", 
                    Title = "perf test", Created = DateTime.Now, Published = DateTime.Now.AddYears(1000) };
                if (post.Tags == null) post.Tags = new List<Tag>();
                post.Tags.Add(new Tag { Name = "perf", Post = post });
                DataSession.SaveOrUpdate(post);
            }
            DataSession.Flush(); //finalizes the db write
            TimeSpan span = DateTime.Now - start;
            System.Diagnostics.Debug.WriteLine("sql insert span: " + span.TotalMilliseconds.ToString());
            System.Diagnostics.Debug.WriteLine("rows/sec: " + (count/span.TotalSeconds).ToString());
        }
    }
}