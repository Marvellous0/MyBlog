using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface ICategoryService
    {
        public Category AddCategory(int id, DateTime createdAt, string name, string imageURL);
        public Category UpdateCategory(int id, DateTime createdAt, string name, string imageURL);
        public void Delete(int id);
        public Category FindById(int? id);
        public Category GetCategoryByName(string categoryName);
        public IEnumerable<Category> GetAllCategories();
    }
}
