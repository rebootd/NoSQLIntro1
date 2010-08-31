using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace BlogSQL.Models.Mapping
{
	public class PostMap : ClassMap<Post>
	{
		public PostMap()
		{
			Table("Posts");
			Id(x => x.Id).GeneratedBy.Guid();
			Map(x => x.Hash);
			Map(x => x.Title);
			Map(x => x.Content);
			Map(x => x.Published);
			Map(x => x.Created);
			HasMany(x => x.Tags).Inverse().Cascade.All();
		}
	}    
}