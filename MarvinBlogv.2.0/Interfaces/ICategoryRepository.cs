using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface ICategoryRepository
    {
        public Category AddCategory(Category category);
        public Category UpdateCategory(Category category);
        public Category FindCategoryById(int? id);
        public Category FindCategoryByName(string categoryName);
        public void Delete(int id);
        public IEnumerable<Category> GetAllCategories();
    }
}
