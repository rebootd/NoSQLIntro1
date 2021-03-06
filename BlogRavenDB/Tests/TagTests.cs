﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogRavenDB.Models;
using Xunit;
using Raven.Client.Document;

namespace BlogRavenDB.Tests
{
    public class TagTests : TestBase
    {
        public Post GetNewPost()
        {
            Post post = new Post { Title = "new title", Hash = "new-title", Content = "test content", Published = DateTime.Now, Created = DateTime.Now };
            post.Tags.Add(new Tag { Name = "yours" });
            post.Tags.Add(new Tag { Name = "mine" });
            return post;
        }

        Post _post = null;

        public TagTests()
        {
            //ensure there's at least one post
            _post = GetNewPost();
            DocumentSession.Store(_post);
            DocumentSession.SaveChanges();
        }

        ~TagTests()
        {
            DocumentSession.Delete<Post>(_post);
            _post = null;
        }

        [Fact]
        public void can_fetch_by_name_index()
        {
            can_fetch_by_index("TagsByName");
        }

        private void can_fetch_by_index(string index)
        {
            var tags = fetch_by_index<Tag>(index);
            Assert.NotEmpty(tags);
        }

        [Fact]  
        public void can_fetch()
        {
            var tags = DocumentSession.LuceneQuery<Tag>("TagsByName").WaitForNonStaleResults().ToList();
            Assert.NotEmpty(tags);
        }

        [Fact]
        public void fecth_unique()
        {
			var alltags = DocumentSession.LuceneQuery<Tag>("TagsByName").WaitForNonStaleResults().ToList();
			List<string> tags   = (from t in alltags
								 select t.Name)
								.ToList();

            Assert.NotEmpty(tags);
        }

        [Fact]
        public void find_posts_by_tag()
        {
            string name = "yours";

			var posts = DocumentSession.LuceneQuery<Post>("PostsByTag")
				.Where(x => x.Published <= DateTime.Now && x.Tags.Any(y => y.Name == name))
				.ToList();
            
            Assert.NotEmpty(posts);
        }
    }
}