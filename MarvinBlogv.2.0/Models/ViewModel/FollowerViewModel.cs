using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Models.ViewModel
{
    public class FollowerViewModel
    {
        public int Id { get; set; }

        public int FollowerId { get; set; }

        public int FollowingId { get; set; }

        public Follower Follower { get; set; }

    }

    public class AddFollowerViewModel
    {
        public int Id { get; set; }

        public DateTime StartedFollowing { get; set; }

        public int FollowingId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
