using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Controllers
{

    public class SuperAdminController : Controller
    {
        private readonly IUserService _userService;
       
        public SuperAdminController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            User user = _userService.FindUserById(userId);

            ViewBag.name = user.FullName;

            return RedirectToAction("Index", "SuperAdmin");
        }
    }
}
