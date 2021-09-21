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
    public class PostCategoryRepository : IPostCategoryRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostCategoryRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PostCategory AddPostCategory(PostCategory postCategory)
        {
            _dbContext.PostCategories.Add(postCategory);
            _dbContext.SaveChanges();
            return postCategory;
        }

        public void Delete(int id)
        {
            var postCategory = FindPostCategory(id);
            {
                if (postCategory != null)
                {
                    _dbContext.PostCategories.Remove(postCategory);
                    _dbContext.SaveChanges();
                }
            }
        }

        public PostCategory FindPostCategory(int id)
        {
            return _dbContext.PostCategories.Find(id);
        }

        public List<PostCategory> GetOverallPostCategories()
        {
            return _dbContext.PostCategories.ToList();
        }

        public List<PostCategory> GetAllPostCategories(int postId)
        {
            var postCategories = _dbContext.PostCategories.Include(c => c.Category).Where(c => c.PostId == postId).ToList();

            List<PostCategory> categories = new List<PostCategory>();

            foreach (var postCategory in postCategories)
            {
                Category category = new Category
                {
                    Id = postCategory.CategoryId,
                    Name = postCategory.Category.Name
                };

                categories.Add(postCategory);
            }

            return categories;

        }

        public PostCategory UpdatePostCategory(PostCategory postCategory)
        {
            _dbContext.PostCategories.Update(postCategory);
            _dbContext.SaveChanges();
            return postCategory;
        }

        public List<PostCategory> GetPostByCategoryId(int categoryId)
        {
            return _dbContext.PostCategories.Include(c => c.Post).Where(c => c.CategoryId == categoryId && c.Post.Status == true).ToList();
        }

        public List<PostCategory> GetCategoryByPostId(int postId)
        {
            return _dbContext.PostCategories.Include(p => p.Category).Where(p => p.PostId == postId).ToList();
        }
    }
}
