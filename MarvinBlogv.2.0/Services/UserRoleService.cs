using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Services
{
    public class UserRoleService : IUserRoleService
    {
        public IUserRoleRepository _userRoleRepository;
        public IUserService _userService;
        public IRoleService _roleService;

        public UserRoleService(IUserRoleRepository userRoleRepository, IUserService userService, IRoleService roleService)
        {
            _userRoleRepository = userRoleRepository;
            _userService = userService;
            _roleService = roleService;
        }

        public UserRole AddUserRole(int id, DateTime createdOn, int userId, int roleId)
        {
            UserRole userRole = new UserRole
            {
                Id = id,
                CreatedAt = DateTime.Now,
                User = _userService.FindUserById(id),
                Role = _roleService.GetRoleById(id)
            };
            return _userRoleRepository.AddUserRole(userRole);
        }

        public void Delete(int id)
        {
            _userRoleRepository.Delete(id);
        }

        public List<Role> FindUserRole(int userId) 
        {
            return _userRoleRepository.GetUserRoles(userId);
        }

        public User FindUserId(int userId) 
        {
            return _userRoleRepository.FindUserId(userId);
        }

        public Role FindRoleId(int roleId)
        {
            return _userRoleRepository.FindRoleId(roleId);
        }

        public UserRole FindById(int id)
        {
            return _userRoleRepository.FindById(id);
        }

        public UserRole FindByName(string name)
        {
            return _userRoleRepository.FindByName(name);
        }

        public UserRole UpdateUserRole(int id, DateTime createdOn, int userId, int roleId)
        {
           var userRole = _userRoleRepository.FindById(id);

            userRole.CreatedAt = DateTime.Now;

            userRole.UserId = userId;

            userRole.RoleId = roleId;

            return _userRoleRepository.UpdateUserRole(userRole);
        }
    }
}
