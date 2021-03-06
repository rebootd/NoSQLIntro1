﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;
using Raven.Database.Indexing;
using BlogRavenDB.Models;

namespace BlogRavenDB.Indexes
{
	public class PostsByHash : AbstractIndexCreationTask
	{
		public override IndexDefinition CreateIndexDefinition()
		{
			return new IndexDefinition<Post, Post>
			{
				Map = posts => from post in posts
							   select new { post.Hash },
				Indexes = { { x => x.Hash, FieldIndexing.NotAnalyzed } }
			}
			.ToIndexDefinition(DocumentStore.Conventions);
		}
	}
}