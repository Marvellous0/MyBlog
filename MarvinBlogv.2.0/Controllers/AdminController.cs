using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarvinBlogv._2._0.Controllers
{
    public class AdminController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IRoleService _roleService;

        public AdminController(IUserService userService, IRoleService roleService, IPostService postService)
        {
            _userService = userService;
            _roleService = roleService;
            _postService = postService;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index(string email)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            User user = _userService.FindUserById(userId);

            ViewBag.name = user.FullName;

            ViewBag.email = user.Email;

            IEnumerable<Post> posts = _postService.UnApprovedPost();

            return View(posts); ;
        }

        public IActionResult ApprovedPost() 
        {
            IEnumerable<Post> approvedPosts = _postService.ApprovedPost();

            return View(approvedPosts);
        }

        public IActionResult UnApprovedPost()
        {
            IEnumerable<Post> unapprovedPosts = _postService.UnApprovedPost();

            return View(unapprovedPosts);
        }
    }
}
