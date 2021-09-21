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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogDbContext _dbContext;

        public CategoryRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Category AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return category;
        }

        public void Delete(int id)
        {
            var category = FindCategoryById(id);
            {
                if (category != null)
                {
                    _dbContext.Categories.Remove(category);
                    _dbContext.SaveChanges();
                }
            }
        }

        public Category FindCategoryById(int? id)
        {
            return _dbContext.Categories.Find(id);
        }

        public Category FindCategoryByName(string categoryName)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Name.Equals(categoryName));
        }

        public Category UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            return category;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }
    }
}
