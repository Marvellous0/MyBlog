using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IFollowerService
    {
        public Follower AddFollower(int userId, int followingId, DateTime startedFollowing);

        public Follower FindFollowingById(int id);

        public void Unfollow(int id);

        public Follower CheckFollow(int userLogId, int posterId);

        public List<Follower> GetFollowersOfUser(int userId);

        public List<Follower> GetFollowingOfUser(int userId);
    }
}
