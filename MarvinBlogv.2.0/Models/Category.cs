using System.Collections.Generic;

namespace MarvinBlogv._2._0.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string ImageURL { get; set; }

        public ICollection<PostCategory> AssociatedPosts { get; set; } = new HashSet<PostCategory>();
    }
}
