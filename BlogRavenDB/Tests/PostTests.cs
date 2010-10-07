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
        public Post GetNewPost()
        {
            Post post = new Post { Title = "new title", Hash = "new-title", Content = "test content", Published = DateTime.Now, Created = DateTime.Now };
            post.Tags.Add(new Tag { Name = "yours" });
            post.Tags.Add(new Tag { Name = "mine" });
            return post;
        }

        Post _post = null;

        public PostTests()
        {
            //ensure there's at least one post
            _post = GetNewPost();
            DocumentSession.Store(_post);
            DocumentSession.SaveChanges();
        }

        ~PostTests()
        {
            DocumentSession.Delete<Post>(_post);
            _post = null;
        }

        [Fact]
        public void can_fetch()
        {
			List<Post> posts = DocumentSession.LuceneQuery<Post>("PostsByPublished")
				.WaitForNonStaleResults()
				.ToList();

            Assert.NotEmpty(posts);
        }

		[Fact]
		public void can_fetch_by_title_index()
		{
			can_fetch_by_index("PostsByTitle");
		}

		[Fact]
		public void can_fetch_by_hash_index()
		{
			can_fetch_by_index("PostsByHash");
		}

        [Fact]
        public void can_fetch_by_tag_index()
        {
            can_fetch_by_index("PostsByTag");
        }

        [Fact]
        public void can_fetch_by_pub_index()
        {
            can_fetch_by_index("PostsByPublished");
        }

        [Fact]
        public void can_fetch_textsearch_index()
        {
            can_fetch_by_index("PostsTextSearch");
        }

        [Fact]
        public void can_textsearch()
        {   
            var posts = DocumentSession.LuceneQuery<Post>("PostsTextSearch")
                .Where("AllText:yours")
                .ToList();
            Assert.NotEmpty(posts);
        }

        //odd bug affect ravendb causes one of these to fail; i believe the first one.. but make sure at least one passes.
        //note: the bug is not in ravendb so far as I can tell; its actually WebRequest.Create(..)

        [Fact]
        public void can_textsearch2()
        {
            var posts = DocumentSession.LuceneQuery<Post>("PostsTextSearch")
                .Where("AllText:ours")
                .ToList();
            Assert.NotEmpty(posts);
        }

		private void can_fetch_by_index(string index)
		{
            var posts = fetch_by_index<Post>(index);
			Assert.NotEmpty(posts);
		}

        [Fact]
        public void can_fetch_by_tag()
        {
            string name = "yours";

            var posts = DocumentSession.LuceneQuery<Post>("PostsByTag")
				.Where(x => x.Published <= DateTime.Now && x.Tags.Any(y => y.Name == name))
				.ToList();

            Assert.NotEmpty(posts);
        }        
    }
}