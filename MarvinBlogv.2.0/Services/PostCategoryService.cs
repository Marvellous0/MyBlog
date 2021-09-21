using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;

namespace MarvinBlogv._2._0.Services
{
    public class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryService _categoryService;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IPostRepository postRepository, ICategoryService categoryService)
        {
            _postCategoryRepository = postCategoryRepository;
            _postRepository = postRepository;
            _categoryService = categoryService;
        }

        public PostCategory AddPostCategory(int id, DateTime createdAt, int postId, int categoryId)
        {
            PostCategory postCategory = new PostCategory
            {
                Id = id,
                CreatedAt = DateTime.Now,
                Post = _postRepository.FindById(postId),
                Category = _categoryService.FindById(categoryId)
            };
            return _postCategoryRepository.AddPostCategory(postCategory);
        }

        public void Delete(int id)
        {
            _postCategoryRepository.Delete(id);
        }

        public PostCategory FindPostCategory(int id)
        {
            return _postCategoryRepository.FindPostCategory(id);
        }

        public List<PostCategory> GetAllPostCategories(int postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostCategory> GetCategoryByPostId(int postId)
        {
            return _postCategoryRepository.GetCategoryByPostId(postId);
        }

        public List<PostCategory> GetOverallPostCategories()
        {
            return _postCategoryRepository.GetOverallPostCategories();
        }

        public IEnumerable<PostCategory> GetPostByCategoryId(int categoryId)
        {
            return _postCategoryRepository.GetPostByCategoryId(categoryId);
        }

        public PostCategory UpdatePostCategory(int id, DateTime createdAt, int postId, int categoryId)
        {
            var postCategory = _postCategoryRepository.FindPostCategory(id);

            postCategory.CreatedAt = DateTime.Now;

            postCategory.Post = _postRepository.FindById(postId);

            postCategory.Category = _categoryService.FindById(categoryId);

            return _postCategoryRepository.UpdatePostCategory(postCategory);
        }
    }
}
