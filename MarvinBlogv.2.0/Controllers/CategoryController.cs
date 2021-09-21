using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using MarvinBlogv._2._0.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using MarvinBlogv._2._0.DTO;
using static MarvinBlogv._2._0.Models.ViewModel.CategoryViewModel;

namespace MarvinBlogv._2._0.Controllers
{
    
  
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserRoleService _userRoleService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IReviewService _reviewService;
        private readonly IFollowerService _followerService;
        public readonly BlogDbContext _db;

        public CategoryController(ICategoryService categoryService, IWebHostEnvironment webHostEnvironment, BlogDbContext db, IPostService postService, IPostCategoryService postCategoryService, IReviewService reviewService, IFollowerService followerService, IUserService userService, IUserRoleService userRoleService)
        {
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
            _postService = postService;
            _postCategoryService = postCategoryService;
            _reviewService = reviewService;
            _followerService = followerService;
            _userService = userService;
            _userRoleService = userRoleService;
        }

        [Authorize(Roles = "admin, blogger")]
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryService.GetAllCategories();

            return View(categories);
        }

        [HttpGet]
        public IActionResult GetPostByCategoryId(int id) 
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var roles = _userRoleService.FindUserRole(userId);

            ViewBag.Role = roles[0].Name;

            User user = _userService.FindUserById(userId);

            ViewBag.name = user.FullName;

            ViewBag.email = user.Email;
            ViewBag.Category = _categoryService.FindById(id).Name;

            var ListPosts = new List<PostDTo>();
            var posts = _postCategoryService.GetPostByCategoryId(id);
            foreach (var post in posts)
            {
                var categories = new List<Category>();
                var following = false;
                int posterId = _postService.FindById(post.PostId).UserId;
                var poster = _userService.FindUserById(posterId);
                if (_followerService.CheckFollow(userId, posterId) != null)
                {
                    following = true;
                }

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
                    CommentCount = _reviewService.CommentCount(post.Id),
                };

                ListPosts.Add(listPost);
            }
            return View(ListPosts);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (model.Id == 0)
            {
                //Creating
                string upload = webRootPath + WC.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName); 
                string filePath = fileName + extension;

                using (var fileStream = new FileStream(Path.Combine(upload, filePath), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                model.ImageURL = fileName + extension;
               
                _categoryService.AddCategory(model.Id, model.CreatedAt, model.Name, model.ImageURL);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Update(int id) 
        {
            var category = _categoryService.FindById(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Update(UpdateCategoryViewModel model) 
        {
            var objFromDb = _db.Categories.AsNoTracking().FirstOrDefault(u => u.Id == model.Id);
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;

            if (files.Count > 0)
            {
                string upload = webRootPath + WC.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);
                string filePath = fileName + extension;

                var oldFile = Path.Combine(upload, objFromDb.ImageURL);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, filePath), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                model.ImageURL = fileName + extension;
            }
            else
            {
                model.ImageURL = objFromDb.ImageURL;
            }

            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            _categoryService.UpdateCategory(model.Id, model.CreatedAt, model.Name, model.ImageURL);

            return RedirectToAction("Index");
        }
    }
}
