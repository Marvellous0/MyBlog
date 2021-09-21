using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IUserService
    {
        public User FindUserById(int id);
        public void RegisterUser(CreateUserDto createUserDto);
        public User LoginUser(string email, string password);
        //public User AddUser(int id, DateTime created, string fullName, string email, string password, int roleId, int postId);
        public User FindUserByEmail(string email);
        //public Role FindRole(string name);
        //public IEnumerable<User> GetUserPosts(int postId);
        //public IEnumerable<Post> GetAllPosts();
        //public IEnumerable<Follower> FindFollower(int followerId);
        //public IEnumerable<Follower> FindFollowing(int followingId);
        //public User FindUserById(int id);

        public IEnumerable<User> GetUsers(int userId);
    }
}
