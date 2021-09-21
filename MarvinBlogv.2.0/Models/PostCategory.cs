using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Models
{
    public class PostCategory : BaseEntity
    {
        public Post Post { get; set; }
        public int PostId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
