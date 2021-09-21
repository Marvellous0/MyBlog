using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MarvinBlogv._2._0.Controllers
{
    public class RoleController : Controller
    {
        private readonly BlogDbContext _context;

        public RoleController(BlogDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddRole()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddRole(int id, string name, DateTime createdAt)
        {

            Role role = new Role
            {
                Id = id,
                Name = name.ToLower(),
                CreatedAt = DateTime.Now
            };

            _context.Roles.Add(role);
            _context.SaveChanges();
            return View();
        }
    }
}
