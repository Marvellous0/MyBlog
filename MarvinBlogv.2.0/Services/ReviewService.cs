using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;

namespace MarvinBlogv._2._0.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public ReviewService(IReviewRepository reviewRepository, IUserService userService, IPostService postService)
        {
            _reviewRepository = reviewRepository;
            _userService = userService;
            _postService = postService;
        }

        public Review AddReview(int userId, bool reaction, int postId, string createdBy)
        {
            Review review = new Review
            {
                CreatedAt = DateTime.Now,
                UserId = userId,
                Reaction = reaction,
                PostId = postId,
                CreatedBy = createdBy,
            };

            _reviewRepository.AddReview(review);

            return review;
        }

        public void Delete(int id)
        {
            _reviewRepository.Delete(id);
        }

        public Review FindReviewById(int? id)
        {
           return _reviewRepository.FindReviewById(id);
        }

        public Review FindReviewer(int reviewerId)
        {
            return _reviewRepository.FindReviewer(reviewerId);
        }

        public Review UpdateReview(int id, DateTime reviewedOn, int userId, bool reaction, int postId, string comment)
        {
            var review = _reviewRepository.FindReviewById(id);

            review.CreatedAt = DateTime.Now;

            review.UserId = userId;

            review.Reaction = reaction;

            review.PostId = postId;

            review.Comment = comment;

            return _reviewRepository.UpdateReview(review);
        }

        public int ReviewCount(int postId)
        {
            return _reviewRepository.ReviewCount(postId);
        }

         public int CommentCount(int postId)
        {
            return _reviewRepository.CommentCount(postId);
        }
        
        public IEnumerable<Review> GetAllComments(int postId)
        {
            return _reviewRepository.GetAllComments(postId);
        }

        public List<Review> FindByPostId(int postId) 
        {
            return _reviewRepository.FindByPostId(postId);
        }

        public Review AddComment(int userId, string comment, int postId, string createdBy)
        {
            Review review = new Review
            {
                CreatedAt = DateTime.Now,
                UserId = userId,
                Comment = comment,
                PostId = postId,
                CreatedBy = createdBy,
            };

            _reviewRepository.AddComment(review);

            return review;
        }

        public List<Review> FindByUserId(int userId)
        {
           return _reviewRepository.FindByUserId(userId);
        }

        public Review CheckLike(int postId, int userId)
        {
            return _reviewRepository.CheckLike(postId, userId);
        }
    }
}
