using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using MarvinBlogv._2._0.Models.ViewModel;
using MarvinBlogv._2._0.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Controllers
{
    public class FollowerController : Controller
    {
        private readonly IFollowerService _followerService;
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public FollowerController(FollowerService followerService, UserService userService, IPostService postService)
        {
            _followerService = followerService;
            _userService = userService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
