using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IUserRoleService
    {
        public UserRole AddUserRole(int id, DateTime createdOn, int userId, int roleId);
        public UserRole UpdateUserRole(int id, DateTime createdOn, int userId, int roleId);
        public void Delete(int id);
        public List<Role> FindUserRole(int userId);
        public UserRole FindById(int id);
        public UserRole FindByName(string name);
    }
}
