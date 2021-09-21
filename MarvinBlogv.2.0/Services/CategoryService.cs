using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;

        public CategoryService(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
        }

        public Category AddCategory(int id, DateTime createdAt, string name, string imageURL)
        {
            Category category = new Category
            {
                Id = id,
                CreatedAt = DateTime.Now,
                Name = name,
                ImageURL = imageURL
            };

            _categoryRepository.AddCategory(category);

            return category;
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
        }

        public Category FindById(int? id)
        {
            return _categoryRepository.FindCategoryById(id);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories().Select(c => new Category
            {
                Id = c.Id,
                CreatedAt = DateTime.Now,
                Name = c.Name,
                ImageURL = c.ImageURL,
            }).ToList();

            return categories;
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _categoryRepository.FindCategoryByName(categoryName);
        }

        public Category UpdateCategory(int id, DateTime createdAt, string name, string imageURL)
        {
            var category = _categoryRepository.FindCategoryById(id);

            category.CreatedAt = createdAt;

            category.Name = name;

            category.ImageURL = imageURL;

            return _categoryRepository.UpdateCategory(category);
        }
    }
}