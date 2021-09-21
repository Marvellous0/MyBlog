using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using MarvinBlogv._2._0.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Controllers
{
    public class PostController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IUserRoleService _userRoleService;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly BlogDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostController(IUserService userService, IPostService postService, ICategoryService categoryService, IPostCategoryService postCategoryService, IWebHostEnvironment webHostEnvironment, BlogDbContext db, IReviewService reviewService, IUserRoleService userRoleService, INotificationService notificationService)
        {
            _userService = userService;
            _postService = postService;
            _categoryService = categoryService;
            _postCategoryService = postCategoryService;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
            _reviewService = reviewService;
            _userRoleService = userRoleService;
            _notificationService = notificationService;
        }

        [Authorize(Roles = "blogger, admin")]
        public IActionResult Index(int postId)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);
            ViewBag.name = user.FullName;
            ViewBag.email = user.Email;
            ViewBag.NotificationCount = _notificationService.GetUserNotificationByUserIdCount(userId);

            var ListPosts = new List<PostDTo>();
            var posts = _postService.GetAllPosts();
            var categories = new List<Category>();
            foreach (var post in posts)
            {
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
                    PostCategories = post.PostCategories.Select(p => p.Category).ToList(),
                    Like = post.Reviews.Where(r => r.Reaction == true).Count(),
                    //Comment = post.Reviews.Where(r => r.PostId == post.Id && r.Comment == post.Comment).Count(),
                };
                ListPosts.Add(listPost);
            }
            return View(ListPosts);
        }
        

        [Authorize(Roles = "blogger, admin")]
        [HttpGet]
        public IActionResult Create()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var roles = _userRoleService.FindUserRole(userId);

            ViewBag.Role = roles[0].Name;
            User user = _userService.FindUserById(userId);
            ViewBag.userId = user.Email;
            ViewBag.id = user.Id;

            CreatePostViewModel vm = new CreatePostViewModel()
            {
                CategorySelectListItem = _categoryService.GetAllCategories().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePostViewModel model)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (model.Id == 0)
            {
                //Creating
                string upload = webRootPath + WC.PostImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);
                string filePath = fileName + extension;

                using (var fileStream = new FileStream(Path.Combine(upload, filePath), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                model.FeaturedImageURL = fileName + extension;
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                _postService.AddBlogPost(model.Id, model.CreatedAt, model.Name, model.Title, model.FeaturedImageURL, model.Content, model.Description, model.PostURL, userId, model.Categories, model.CreatedBy, model.Status);
            }

            return RedirectToAction("Index", "Blogger");
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult ApprovePost(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            var posts = _postService.FindById(id);

            int posterId = _postService.FindById(posts.Id).UserId;

            var poster = _userService.FindUserById(posterId);

            _postService.UpdatePost(posts.Id, posts.CreatedAt, posts.Title, posts.FeaturedImageURL, posts.Content, posts.PostCategories, posts.Description, posts.PostURL, true);

            var CreateModel = new CreateNotificationDTO
            {
                Message = $"Post made on {posts.CreatedAt.ToLongDateString()} with Title: '{posts.Title}' has been Approved by the Admin @ {DateTime.Now}",
                UserId = posterId,
                PostId = posts.Id,
                CreatedAt = DateTime.Now,
                CreatedBy = user.Email,
                Type = "ApprovePost",
                ImageURL = posts.FeaturedImageURL,
                
            };

            _notificationService.AddNotification(CreateModel);

            return RedirectToAction("UnApprovedPost", "Blogger");
        }


        public IActionResult GetByUserNotificationList(int id)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            var post = _postService.FindById(userId);
            ViewBag.name = user.FullName;
            ViewBag.PostId = id;
            ViewBag.email = user.Email;
            ViewBag.NotificationCount = _notificationService.GetUserNotificationByUserIdCount(userId);


            List<NotificationList> notificationLists = new List<NotificationList>();

            var notifications = _notificationService.GetNotificationByUserId(userId);


            foreach (var notification in notifications) 
            {
                NotificationList notificationList = new NotificationList
                {
                    Id = notification.Id,
                    Message = notification.Message,
                    UserId = userId,
                    PostId = notification.PostId ?? 0,
                    CreatedAt = DateTime.Now,
                    CreatedBy = _userService.FindUserByEmail(notification.CreatedBy).Id,
                    ImageURL = notification.ImageURL,
                    Type = notification.Type,
                    NotificationCount = _notificationService.GetUserNotificationByUserIdCount(userId)
                };

                notificationLists.Add(notificationList);
            }
           
            return View(notificationLists);
        }

        [Authorize]
        public IActionResult SeenNotification(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var notification = _notificationService.FindById(id);

            if (notification == null)
            {
                return NotFound();
            }

            _notificationService.Delete(id);
            return RedirectToAction("GetByUserNotificationList", "Post");
        }


        [Authorize]
        [HttpGet]
        public IActionResult Update(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var post = _postService.FindById(id);

            if (post == null)
            {
                return NotFound();
            }
           
            var vm = new UpdatePostViewModel()
            {
                PostCategories = post.PostCategories,
                PostURL = post.PostURL,
                FeaturedImageURL = post.FeaturedImageURL,
                Id = post.Id,
                CreatedBy = post.CreatedBy,
                CreatedAt = DateTime.Now,
                Content = post.Title,
                Description = post.Description,
                Title = post.Title,

                CategorySelectListItem = _categoryService.GetAllCategories().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),

            };
           
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(UpdatePostViewModel model)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var objFromDb = _db.Posts.AsNoTracking().FirstOrDefault(u => u.Id == model.Id);
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;

            if (files.Count > 0)
            {
                string upload = webRootPath + WC.PostImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);
                string filePath = fileName + extension;

                var oldFile = Path.Combine(upload, objFromDb.FeaturedImageURL);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, filePath), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                model.FeaturedImageURL = fileName + extension;
            }
            else
            {
                model.FeaturedImageURL = objFromDb.FeaturedImageURL;
            }
            _postService.UpdatePost(model.Id, model.CreatedAt, model.Title, model.FeaturedImageURL, model.Content, model.PostCategories, model.Description, model.PostURL, false);
            return RedirectToAction("PendingPostPerUser","Blogger");
        }

        [Authorize]
        public IActionResult DeletePost(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var post = _postService.FindById(id);

            if (post == null)
            {
                return NotFound();
            }

            _postService.Delete(id);
            return RedirectToAction("PendingPostPerUser", "Blogger");
        }


        [Authorize]
        public IActionResult DeleteNotification(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var notification = _notificationService.FindById(id);

            if (notification == null)
            {
                return NotFound();
            }

            _notificationService.Delete(id);
            return RedirectToAction("GetByUserNotificationList");
        }


        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile aUploadedFile)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var vImageSavePath = string.Empty;
            string img = "";

            if (aUploadedFile.Length > 0)
            {
                var vFileName = Path.GetFileNameWithoutExtension(aUploadedFile.FileName);
                var vExtension = Path.GetExtension(aUploadedFile.FileName);

                string sImageName = vFileName + "-" + "image";
                img = WC.ContentImagePath + sImageName + vExtension;
                vImageSavePath = Path.Combine(_webHostEnvironment.ContentRootPath + "\\wwwroot\\" + "\\UploadFiles\\" + sImageName + vExtension);
                //vReturnImagePath = "/UploadFiles/" + sImageName + vExtension;
                ViewBag.Msg = vImageSavePath;
                var path = vImageSavePath;

                // Saving Image in Original Mode
                using (var stream = new FileStream(vImageSavePath, FileMode.Create))
                {
                    await aUploadedFile.CopyToAsync(stream);
                }
                var vImageLength = new FileInfo(path).Length;

            }
            //here to add Image Path to You Database,
            TempData["message"] = string.Format("Image was Added Successfully");
            return Json(img);
        }

        [HttpGet]
        public IActionResult GetCategoryByPostId(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var postsCategory = _postCategoryService.GetCategoryByPostId(id);

            List<Category> Categories = new List<Category>();
            foreach (var item in postsCategory)
            {
                var category = _categoryService.FindById(item.CategoryId);
                Categories.Add(category);
            }

            return View(Categories);
        }
     

  
    }
}
