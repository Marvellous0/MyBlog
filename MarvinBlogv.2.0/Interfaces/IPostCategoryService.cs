using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IPostCategoryService
    {
        public PostCategory AddPostCategory(int id, DateTime createdAt, int postId, int categoryId);
        public void Delete(int id);
        public PostCategory UpdatePostCategory(int id, DateTime createdAt, int postId, int categoryId);
        public List<PostCategory> GetAllPostCategories(int postId);
        public PostCategory FindPostCategory(int id);

        public List<PostCategory> GetOverallPostCategories();

        public IEnumerable<PostCategory> GetPostByCategoryId(int categoryId);
        public IEnumerable<PostCategory> GetCategoryByPostId(int postId);
    }
}
