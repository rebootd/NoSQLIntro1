using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;
using Raven.Database.Indexing;
using BlogRavenDB.Models;

namespace BlogRavenDB.Indexes
{
	public class PostsByTitle : AbstractIndexCreationTask
	{
		public override IndexDefinition CreateIndexDefinition()
		{
			return new IndexDefinition<Post>
			{
				Map = posts => from post in posts
							   select new { post.Title }
			}
			.ToIndexDefinition(DocumentStore.Conventions);
		}
	}
}