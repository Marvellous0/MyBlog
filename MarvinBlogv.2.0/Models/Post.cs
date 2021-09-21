using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MarvinBlogv._2._0.Models
{
    public class Post : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        public string Description { get; set; }

        public string FeaturedImageURL { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Notification> Notifications { get; set; }

        public string PostURL { get; set; }

        public User User{ get; set; }

        public int UserId { get; set; } 
        
        public bool Status { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; } = new HashSet<PostCategory>();

    }
}
