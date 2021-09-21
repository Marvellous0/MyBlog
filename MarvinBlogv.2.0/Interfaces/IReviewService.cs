using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IReviewService
    {
        public Review AddReview(int userId, bool reaction, int postId, string createdBy);
        public Review AddComment(int userId, string comment, int postId, string createdBy);
        public Review FindReviewer(int reviewerId);
        public Review FindReviewById(int? id);
        public IEnumerable<Review> GetAllComments(int postId);
        public int CommentCount(int postId);
        public Review UpdateReview(int id, DateTime reviewedOn, int userId, bool reaction, int postId, string comment);
        public void Delete(int id);
        public int ReviewCount(int postId);
        public List<Review> FindByPostId(int postId);
        public List<Review> FindByUserId(int userId);

        public Review CheckLike(int postId, int userId);
    }
}
