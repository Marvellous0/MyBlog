using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IReviewRepository
    {
        public Review AddReview(Review review);
        public Review AddComment(Review review);
        public Review FindReviewer(int reviewerId);
        public int CommentCount(int postId);
        public IEnumerable<Review> GetAllComments(int postId);
        public Review FindReviewById(int? id);
        public Review UpdateReview(Review review);
        public void Delete(int id);
        public List<Review> FindByPostId(int postId);
        public List<Review> FindByUserId(int userId);
        public int ReviewCount(int postId);

        public Review CheckLike(int postId, int userId);
    }
}
