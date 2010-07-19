using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSQL.Models
{
    public class Post
    {
        public virtual Guid Id { get; private set; }
        public virtual string Hash { get; private set; }
        public virtual string Title { get; private set; }
        public virtual string Content { get; private set; }
        public virtual DateTime Published { get; private set; }
        public virtual DateTime Created { get; private set; }
    }
}