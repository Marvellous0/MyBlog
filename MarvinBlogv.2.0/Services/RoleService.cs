using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;

namespace MarvinBlogv._2._0.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role AddRole(DateTime createdAt, string name, int id)
        {
            Role role = new Role
            {
                Id = id,
                CreatedAt = DateTime.Now,
                Name = name,
            };

            _roleRepository.AddRole(role);

            return role;
        }

        public void Delete(int id)
        {
            _roleRepository.Delete(id);
        }

        public Role GetRoleById(int id)
        {
           return _roleRepository.GetRoleById(id);
        }

        public Role GetRoleByName(string roleName)
        {
           return _roleRepository.GetRoleByName(roleName);
        }
    }
}
