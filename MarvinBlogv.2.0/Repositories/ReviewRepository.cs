using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BlogDbContext _dbContext;

        public ReviewRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Review AddReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();
            return review;
        }

        public void Delete(int id)
        {
            var review = FindReviewById(id);
            {
                if (review != null)
                {
                    _dbContext.Reviews.Remove(review);
                    _dbContext.SaveChanges();
                }
            }
        }

        public Review FindReviewById(int? id)
        {
            return _dbContext.Reviews.Find(id);
        }

        public Review FindReviewer(int reviewerId)
        {
            return _dbContext.Reviews.FirstOrDefault(r => r.UserId == reviewerId);
        }
        
        public IEnumerable<Review> GetAllComments(int postId)
        {
            return _dbContext.Reviews.Include(p => p.Post).Where(p => p.PostId == postId && p.Comment != null).ToList();
        }
        
        public Review UpdateReview(Review review)
        {
            _dbContext.Reviews.Update(review);
            _dbContext.SaveChanges();
            return review;
        }

        public List<Review> FindByPostId(int postId)
        {
            return _dbContext.Reviews.Where(review => review.PostId == postId && review.Reaction == true).ToList();
        }

        public Review CheckLike(int postId, int userId)
        {
            return _dbContext.Reviews.FirstOrDefault(review => review.PostId == postId && review.UserId == userId && review.Reaction == true);
        }


        public List<Review> FindByUserId(int userId)
        {
            return _dbContext.Reviews.Where(review => review.UserId == userId).ToList();
        }

        public int ReviewCount(int postId)        
        {
           return _dbContext.Reviews.Where(r => r.PostId == postId && r.Reaction == true).Count();
        }

        public int CommentCount(int postId)        
        {
           return _dbContext.Reviews.Where(r => r.PostId == postId && r.Comment != null).Count();
        }
        
        public Review AddComment(Review review)
        {
            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();
            return review;
        }
    }
}
