using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;
using Raven.Database.Indexing;
using BlogRavenDB.Models;

namespace BlogRavenDB.Indexes
{
    /*
     * This is the index I'm trying to create
     * docs.Posts 
        .Select(post => new {Title = post.Title, AllText = post}) 
     */
    public class PostsTextSearch : AbstractIndexCreationTask
    {
        public override IndexDefinition CreateIndexDefinition() 
        {
            return new IndexDefinition<Post>
            {
                Map = posts => from post in posts
                               select new { Title = post.Title, AllText = post }
            }
            .ToIndexDefinition(DocumentStore.Conventions); 
        }   
    }
}