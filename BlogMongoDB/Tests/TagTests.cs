using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
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
			var collPosts = CurrentMongoSession.GetCollection<Post>();
            var posts = collPosts.Find(new { Published = Q.LessOrEqual(DateTime.Now) }).ToList();
            var ts = from p in posts
                        from t in p.Tags
                        where p.Tags != null
                        select t;
            tags = ts.ToList<Tag>();

            Assert.NotEmpty(tags);
        }

        [Fact]
        public void fecth_unique()
        {

			string map = "function(){ this.Tags.forEach(function(z){ emit( z , { count : 1 } ); } ); };";
			string reduce = "function(key,values){ return 1; };";
			var mr = CurrentMongoSession.Database.CreateMapReduce();
			var query = new { Published = Q.LessOrEqual(DateTime.Now) };
			var response = mr.Execute(new MapReduceOptions<Post> { Map = map, Reduce = reduce, Query = query });
			var alltags = response.GetCollection<TagReduce>().Find().ToList();
			var unique = (from t in alltags
						  select t.Id)
						 .ToList<Tag>();
			
			Assert.NotEmpty(unique);
        }

        [Fact]
        public void find_posts_by_tag()
        {
            string name = "Mine";

            List<Post> posts = new List<Post>();
            var col = CurrentMongoSession.GetCollection<Post>();
            var postCol = col.Find();
            var matches = from p in postCol
                            where p.Tags != null
                            && p.Tags.Any(t => t.Name == name)
                            select p;

            foreach (var entry in matches)
            {
                posts.Add((Post)entry);
            }
            
            //foreach (Tag tag in tags)
            //    posts.Add(tag.Post);

            Assert.NotEmpty(posts);
        }
    }
}