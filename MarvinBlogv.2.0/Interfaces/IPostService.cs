using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IPostService
    {
        public void AddBlogPost(int id, DateTime publishedOn, string name, string title, string featuredImageURL, string content, string description, string postURL, int userId, string[] categoryIds, string createdBy, bool status);
        public Post FindById(int? id);
        public User FindByUser(int userId);
        public List<PostCategory> GetAllPostCategories(int postId);

        public List<Post> GetPostByUserId(int userId);

        public IEnumerable<Post> GetPendingPostByUserId(int userId);
        public IEnumerable<Post> GetAllPosts();
        public List<Post> ApprovedPost();
        public IEnumerable<Post> UnApprovedPost();
        public IEnumerable<Review> GetAllPostReviews(int postId);
        public Post UpdatePost(int id, DateTime createdAt, string title, string featuredImageURL, string content, ICollection<PostCategory> categoryIds, string description, string postURL, bool status);
        public void Delete(int id);
    }
}
