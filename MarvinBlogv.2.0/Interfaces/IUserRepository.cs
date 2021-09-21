using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IUserRepository
    {
        public User AddUser(User user);
        public User FindUserByEmail(string email);
        public Role FindRole(string name);
       // public IEnumerable<User> GetUserPosts(int postId);
        public IEnumerable<Post> GetAllPosts();
        public IEnumerable<Follower> FindFollower(int followerId);
        public IEnumerable<Follower> FindFollowing(int followingId);
        public User FindUserById(int id);

        public List<User> GetUsers(int userId);
    }
}
