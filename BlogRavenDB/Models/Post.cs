using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogRavenDB.Models
{
    public class Post
    {
        public virtual string Id { get; set; }
        public virtual string Hash { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime Published { get; set; }
        public virtual DateTime Created { get; set; }
    }
}