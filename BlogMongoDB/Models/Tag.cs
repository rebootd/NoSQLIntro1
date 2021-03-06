﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Norm;

namespace BlogMongoDB.Models
{
    public class Tag
    {
		public virtual string Name { get; set; }
    }

	public class TagReduce
	{
		public virtual Tag Id { get; set; }
		public virtual int Value { get; set; }
	}
}