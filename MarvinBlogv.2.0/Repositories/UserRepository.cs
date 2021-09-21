using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _dbContext;

        public UserRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public IEnumerable<Follower> FindFollower(int followerId)
        {
            return _dbContext.Followers.Where(follower => follower.FollowingId == followerId).ToList();
        }

        public IEnumerable<Follower> FindFollowing(int followingId)
        {
            return _dbContext.Followers.Where(following => following.FollowingId == followingId).ToList();
        }

        public List<User> GetUsers(int userId)
        {
            return _dbContext.Users.Where(u=> u.Id != 1 && u.Id != userId).ToList();
        }

        public IEnumerable<Post> GetAllPosts() 
        {
            return _dbContext.Posts.ToList();
        }

        //public IEnumerable<User> GetUserPosts(int postId)
        //{
        //    return _dbContext.Users.Where(user => user.PostId == postId).ToList();
        //}

        public Role FindRole(string name)
        {
            return _dbContext.Roles.Find(name);
        }

        public User FindUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Email == email);
        }

        public User FindUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }
    }
}
