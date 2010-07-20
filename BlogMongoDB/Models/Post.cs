using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Norm;

namespace BlogMongoDB.Models
{
    public class Post
    {
        [MongoIdentifier]
        public virtual Guid Id { get; set; }
        public virtual string Hash { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime Published { get; set; }
        public virtual DateTime Created { get; set; }
    }
}