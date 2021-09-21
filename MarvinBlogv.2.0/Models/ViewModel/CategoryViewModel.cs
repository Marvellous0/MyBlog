using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Models.ViewModel
{
    public class CategoryViewModel
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

        public class CreateCategoryViewModel 
        {
            public int Id { get; set; }
            [Required(ErrorMessage = "Category name is required")]
            [Display(Name = "Category Name:")]
            public string Name { get; set; }

            public DateTime CreatedAt { get; set; }

            [Required(ErrorMessage = "Image is required")]
            [Display(Description = "ImageURL")]
            public string ImageURL { get; set; }
        }

        public class UpdateCategoryViewModel
        {
            public int Id { get; set; }

            public DateTime CreatedAt { get; set; }

            [Display(Name = "Category Name:")]
            public string Name { get; set; }

            [Display(Description = "ImageURL:")]
            public string ImageURL { get; set; }
        }
    }
}
