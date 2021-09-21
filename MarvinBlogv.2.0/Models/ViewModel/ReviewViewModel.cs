using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Models.ViewModel
{
    public class ReviewViewModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int CreatedById { get; set; }
        public int? Reaction { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; }
    }

    public class CreateReviewViewModel 
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int CreatedById { get; set; }
        public int? Reaction { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; }
    }

    public class LikeList
    {
        public string FullName { get; set; }

        public int UserId { get; set; }

        public bool IsFollowing { get; set; }

        public string PosterRole { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class NotificationList
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public int UserId { get; set; } 

        public int CreatedBy { get; set; }

        public int NotificationCount { get; set; }

        public int PostId { get; set; }

        public string Type { get; set; }

        public string ImageURL { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class CommentList
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; }
        public int CommentCount { get; set; }
        public string PosterRole { get; set; }
        public bool IsFollowing { get; set; }
        public int UserId { get; set; }

    }
}
