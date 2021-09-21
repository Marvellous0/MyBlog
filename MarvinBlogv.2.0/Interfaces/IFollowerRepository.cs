using MarvinBlogv._2._0.Models;
using System.Collections.Generic;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IFollowerRepository
    {
        public Follower AddFollower(Follower follower);
        public Follower FindById(int id);
        public void Unfollow(int id);

        public Follower CheckFollow(int userLogId, int posterId);

        public List<Follower> GetFollowersOfUser(int userId);

        public List<Follower> GetFollowingOfUser(int userId);
    }
}
