using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;
using Raven.Database.Indexing;
using BlogRavenDB.Models;

namespace BlogRavenDB.Indexes
{
    public class TagsByName : AbstractIndexCreationTask
    {
        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition<Post, TagCount>
            {
                Map = docs => from doc in docs
                              select new { doc.Tags }
                                  into dc
                                  from tag in dc.Tags
                                  select new { tag.Name, Count = 1 },
                Reduce = results => from result in results
                                    group result by result.Name
                                        into g
                                        select new { Name = g.Key, Count = g.Sum(x => x.Count) },
                Stores =
            	{
            		{ x => x.Name, FieldStorage.Yes },
					{ x => x.Count, FieldStorage.Yes }
            	}
            }
            .ToIndexDefinition(DocumentStore.Conventions);
        }

        public class TagCount
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }
    }
}