using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Models.ViewModel
{
    public class PostCategoryIndexModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string CreatedBy { get; set; }
        public List<PostCategory> AssociatedPosts { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int CategoryId { get; set; }
    }
    public class PostViewModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public List<PostCategory> Categories { get; set; }
        public string FeaturedImageURL { get; set; }
        public List<Review> Reviews { get; set; }
        public string PostURL { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
    }

    public class CreatePostViewModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }

        [Required(ErrorMessage = "Post Title is required")]
        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Post Content is required")]
        [Display(Name = "Content:")]
        public string Content { get; set; }

        public string Description { get; set; }
        public string FeaturedImageURL { get; set; }
        public string PostURL { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Select at least one Category")]
        public string[] Categories { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> CategorySelectListItem { get; set; }

    }

    
    public class UpdatePostViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Display(Name = "Content:")]
        public string Content { get; set; }
        public string PostURL { get; set; }
        public string FeaturedImageURL { get; set; }
        public string Description { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; } = new HashSet<PostCategory>();
        public bool Status { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> CategorySelectListItem { get; set; }
    }

    public class ListPostVM
    {

    }

    public class CreateNotificationVM
    {
        public string Message { get; set; }

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int PosterId { get;set; }
    }

    public class PostNotificationVM
    {
        public string Message { get; set; }

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int PosterId { get; set; }
    }
}
