using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IPostRepository
    {
        public Post AddBlogPost(Post post);
        public Post FindById(int? id);
        public User FindByUser(int userId);

        public List<Post> GetPostByUserId(int userId);
        public List<PostCategory> GetAllPostCategories(int postId);
        public IEnumerable<Post> ApprovedPost();
        public IEnumerable<Post> UnApprovedPost();
        public IEnumerable<Post> GetAllPosts();
        public IEnumerable<Review> GetAllPostReviews(int postId);
        public Post UpdatePost(Post post);
        public void Delete(int id);

        public List<Post> GetPendingPostByUserId(int userId);

        public IList<Post> Search(string searchText);
    }
}
