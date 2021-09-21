using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.DTO
{
    public class AddFollowerDTO
    {
        public int Id { get; set; }

        public DateTime StartedFollowing { get; set; }

        public int FollowingId { get; set; }

        public string Email { get; set; }

        public int FollowerId { get; set; }
    }
}
