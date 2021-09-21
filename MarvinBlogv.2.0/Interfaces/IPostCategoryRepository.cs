using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IPostCategoryRepository
    {
        public PostCategory AddPostCategory(PostCategory postCategory);
        public void Delete(int id);
        public PostCategory UpdatePostCategory(PostCategory postCategory);
        public List<PostCategory> GetAllPostCategories(int postId);
        public PostCategory FindPostCategory(int id);

        public List<PostCategory> GetOverallPostCategories();

        public List<PostCategory> GetPostByCategoryId(int categoryId);
        public List<PostCategory> GetCategoryByPostId(int postId);
    }
}
