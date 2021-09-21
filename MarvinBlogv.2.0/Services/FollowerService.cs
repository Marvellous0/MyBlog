using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;

namespace MarvinBlogv._2._0.Services
{
    public class FollowerService : IFollowerService
    {
        private readonly IUserService _userService;
        private readonly IFollowerRepository _followerRepository;
        private readonly IPostRepository _post;

        public FollowerService(IUserService userService, IFollowerRepository followerRepository, IPostRepository post)
        {
            _userService = userService;
            _followerRepository = followerRepository;
            _post = post;
        }

        public Follower AddFollower(int userId, int followingId, DateTime startedFollowing)
        {
            Follower follower = new Follower()
            {
                FollowingId = followingId,

                StartedFollowing = startedFollowing,

                UserId = userId,

            };

            return _followerRepository.AddFollower(follower);
        }

        public Follower CheckFollow(int userLogId, int posterId)
        {
            return _followerRepository.CheckFollow(userLogId, posterId);
        }

        public List<Follower> GetFollowersOfUser(int userId)
        {
            return _followerRepository.GetFollowersOfUser(userId);
        }

        public List<Follower> GetFollowingOfUser(int userId)
        {
            return _followerRepository.GetFollowingOfUser(userId);
        }

        public void Unfollow(int id)
        {
            _followerRepository.Unfollow(id);
        }

        public Follower FindFollowingById(int id)
        {
            return _followerRepository.FindById(id);
        }
    }
}
