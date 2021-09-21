using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MarvinBlogv._2._0.Models;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models.ViewModel;
using MarvinBlogv._2._0.Services;

namespace MarvinBlogv._2._0.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly BlogDbContext db;
        private readonly IUserService _userService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly IUserRoleService _userRoleService;
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;

        public HomeController(ILogger<HomeController> logger, IPostService postService, BlogDbContext _db,
            IUserService userService, IPostCategoryService postCategoryService, IUserRoleService userRoleService, ICategoryService categoryService, IReviewService reviewService)
        {
            _logger = logger;
            _postService = postService;
            db = _db;
            _userService = userService;
            _postCategoryService = postCategoryService;
            _userRoleService = userRoleService;
            _categoryService = categoryService;
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {

            var userId = User.Identity.IsAuthenticated
                ? Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value)
                : 0;
            
            var posts = _postService.ApprovedPost();
            var categories = _categoryService.GetAllCategories();
            var dtoPosts = new List<PostDTo>();
            var users = _userService.GetUsers(userId).Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName
            });

            foreach (var post in posts)
            {
                var following = false;
                int posterId = _postService.FindById(post.Id).UserId;
                var posterRole = _userRoleService.FindUserRole(posterId)[0].Name;
                var poster = _userService.FindUserById(posterId);

                var listPost = new PostDTo
                {
                    Id = post.Id,
                    PostTitle = post.Title,
                    Content = post.Content,
                    Description = post.Description,
                    PostUrl = post.PostURL,
                    CreatedAt = post.CreatedAt,
                    CreatedBy = post.CreatedBy,
                    ImageUrl = post.FeaturedImageURL,
                    Status = post.Status,
                    Created = posterId,
                    IsFollowing = following,
                    PosterRole = posterRole,
                    PosterFullName = poster.FullName,
                    PostCategories =post.PostCategories.Select(pc => pc.Category).ToList(),
                };

                dtoPosts.Add(listPost);

            }
            
            var homeVm = new HomeVM
            {
                Posts = dtoPosts,
                Categories = categories.ToList(),
                Users = users.ToList()
                
            };

            return View(homeVm);
        }

        public IActionResult Detail(int id)
        {

            ViewBag.PostId = _postService.FindById(id);
            var ListPosts = new List<PostDTo>();

            var post = _postService.FindById(id);
            if (post == null)
            {
                return NotFound();
            }

            int posterId = _postService.FindById(post.Id).UserId;
            var poster = _userService.FindUserById(posterId);

            var listPost = new PostDTo
            {
                Id = post.Id,
                PostTitle = post.Title,
                Content = post.Content,
                Description = post.Description,
                PostUrl = post.PostURL,
                CreatedAt = post.CreatedAt,
                CreatedBy = post.CreatedBy,
                ImageUrl = post.FeaturedImageURL,
                Status = post.Status,
                Created = posterId,
                PosterFullName = poster.FullName,
                PostCategories = post.PostCategories.Select(p => p.Category).ToList(),
                Like = _reviewService.ReviewCount(post.Id),
                CommentCount = _reviewService.CommentCount(post.Id)
            };

            ListPosts.Add(listPost);
            return View(listPost);

        }


        public IActionResult GetPostByCategoryId(int id)
        {
           
            ViewBag.Category = _categoryService.FindById(id).Name;

            var ListPosts = new List<PostDTo>();
            var posts = _postCategoryService.GetPostByCategoryId(id);
            foreach (var post in posts)
            {
                var postsCategory = _postCategoryService.GetCategoryByPostId(post.Id);

                var categories = new List<Category>();
                var following = false;
                int posterId = _postService.FindById(post.PostId).UserId;
                var poster = _userService.FindUserById(posterId);
              
                var listPost = new PostDTo()
                {
                    Id = post.Id,
                    PostTitle = post.Post.Title,
                    Content = post.Post.Content,
                    Description = post.Post.Description,
                    PostUrl = post.Post.PostURL,
                    CreatedAt = post.Post.CreatedAt,
                    CreatedBy = post.Post.CreatedBy,
                    ImageUrl = post.Post.FeaturedImageURL,
                    Created = posterId,
                    Status = post.Post.Status,
                    IsFollowing = following,
                    PosterFullName = poster.FullName,
                    PostCategories = post.Post.PostCategories.Select(p => p.Category).ToList(),
                    Like = _reviewService.ReviewCount(post.Id),
                };

                ListPosts.Add(listPost);
            }
            return View(ListPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
