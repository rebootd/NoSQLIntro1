using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSQL.Models
{
	public class Tag
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual Post Post { get; set; }        
	}
}