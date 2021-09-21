using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BlogDbContext _dbContext;

        public RoleRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Role AddRole(Role role)
        {
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();
            return role;
        }

        public void Delete(int id)
        {
            var role = GetRoleById(id);
            {
                if (role != null)
                {
                    _dbContext.Roles.Remove(role);
                    _dbContext.SaveChanges();
                }
            }
        }

        public Role GetRoleById(int id)
        {
            return _dbContext.Roles.Find(id);
        }

        public Role GetRoleByName(string roleName)
        {
            return _dbContext.Roles.FirstOrDefault(r=> r.Name.Equals(roleName));
        }
    }
}
