using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MarvinBlogv._2._0.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
  
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public User FindUserById(int id)
        {
            return _userRepository.FindUserById(id);
        }

        public User FindUserByEmail(string email)
        {
            return _userRepository.FindUserByEmail(email);
        }

        public User LoginUser(string email, string password)
        {
            User user = _userRepository.FindUserByEmail(email);

            if (user == null)
            {
                Console.WriteLine("User not found");
                return null;
            }

            string hashedPassword = HashPassword(password, user.HashSalt);

            if (user.PasswordHash.Equals(hashedPassword))
            {
                Console.WriteLine("User is found");
                return user;
            }

            return null;
        }

        public void RegisterUser(CreateUserDto createUser)
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);

            string hashedPassword = HashPassword(createUser.Password, saltString);

            var role = _roleRepository.GetRoleByName("blogger").Id;

            Console.WriteLine(role);


            var userRoles = new List<UserRole>
            {
                new UserRole
                {
                    UserId = createUser.Id,
                    RoleId = role,
                }
            };

                User user = new User
                {
                Email = createUser.Email,
                FullName = createUser.FullName,
                CreatedAt = createUser.CreatedAt,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
                userRoles = userRoles,
                };

            _userRepository.AddUser(user);
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            Console.WriteLine($"Hashed: {hashed}");

            return hashed;
        }

        public IEnumerable<User> GetUsers(int userId)
        {
            return _userRepository.GetUsers(userId);
        }
    }
}
