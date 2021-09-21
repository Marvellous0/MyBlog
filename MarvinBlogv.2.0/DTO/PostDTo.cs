using System;
using System.Collections.Generic;
using MarvinBlogv._2._0.Models;

namespace MarvinBlogv._2._0.DTO
{
    public class PostDTo
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public TimeSpan CurrentTime { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public int CommentCount { get; set; }
        public int CurrentDate { get; set; }
        public string Message { get; set; }

        public string PosterRole { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public int Like { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Comment { get; set; }
        public string ImageUrl { get; set; }

        public string PostUrl { get; set; }

        public string PosterFullName { get; set; }

        public int Created { get; set; }

        public bool IsFollowing { get; set; }

        public List<Category> PostCategories { get; set; }

        public List<Post> PostPerCategories { get; set; }

        public List<Review> PostReviews { get; set; }
    }
}