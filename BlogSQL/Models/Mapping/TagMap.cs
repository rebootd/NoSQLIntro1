using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace BlogSQL.Models.Mapping
{
	public class TagMap : ClassMap<Tag>
	{
		public TagMap()
		{
			Table("Tags");
			Id(x => x.Id).GeneratedBy.Guid();
			Map(x => x.Name);
			References(x => x.Post);
		}
	}
}