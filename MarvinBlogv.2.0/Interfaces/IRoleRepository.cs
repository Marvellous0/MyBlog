using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Interfaces
{
    public interface IRoleRepository
    {
        public Role AddRole(Role role);

        public Role GetRoleById(int id);

        public Role GetRoleByName(string roleName);

        public void Delete(int id);
    }
}
