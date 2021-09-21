using System;
using System.Collections.Generic;

namespace MarvinBlogv._2._0.Models
{
    public class Follower 
    {
        public User User { get; set; }

        public int UserId { get; set; }

        public int Id { get; set; }
        public int FollowingId { get; set; }
        public DateTime StartedFollowing { get; set; }

 
    }
}
