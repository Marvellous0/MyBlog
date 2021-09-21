using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using MarvinBlogv._2._0.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NikeFarms.v2._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MarvinBlogv._2._0.Controllers
{
    //[Authorize(Roles = "blogger")]
    public class BloggerController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IUserRoleService _userRoleService;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly IFollowerService _followerService;

        public BloggerController(IUserService userService, INotificationService notificationService, IPostService postService, ICategoryService categoryService, IPostCategoryService postCategoryService, IReviewService reviewService, IFollowerService followerService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _postService = postService;
            _categoryService = categoryService;
            _notificationService = notificationService;
            _postCategoryService = postCategoryService;
            _reviewService = reviewService;
            _followerService = followerService;
            _userRoleService = userRoleService;
        }

        [Authorize]
        public IActionResult Index()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            User user = _userService.FindUserById(userId);

            var roles = _userRoleService.FindUserRole(userId);

            ViewBag.Role = roles[0].Name;

            ViewBag.name = user.FullName;

            ViewBag.email = user.Email;

            ViewBag.NotificationCount = _notificationService.GetUserNotificationByUserIdCount(userId);

            var ListPosts = new List<PostDTo>();
            var posts = _postService.ApprovedPost();

            foreach (var post in posts)
            {
                
                var following = false;
                int posterId = _postService.FindById(post.Id).UserId;
                var posterRole = _userRoleService.FindUserRole(posterId)[0].Name;
                var poster = _userService.FindUserById(posterId);
                
                if (_followerService.CheckFollow(userId, posterId) != null)
                {
                    following = true;
                }

                if(ViewBag.Role == "SuperAdmin")
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
                        Created = posterId,
                        IsFollowing = following,
                        PosterRole = posterRole,
                        PosterFullName = poster.FullName,
                        PostCategories = post.PostCategories.Select(p => p.Category).ToList(),
                        Like = _reviewService.ReviewCount(post.Id),
                        CommentCount = _reviewService.CommentCount(post.Id)
                    };

                    ListPosts.Add(listPost);
                }
                else
                {
                    if (following == true || posterRole == "SuperAdmin" || posterId == userId)
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
                            Created = posterId,
                            IsFollowing = following,
                            PosterRole = posterRole,
                            PosterFullName = poster.FullName,
                            PostCategories = post.PostCategories.Select(p => p.Category).ToList(),
                            Like = _reviewService.ReviewCount(post.Id),
                            CommentCount = _reviewService.CommentCount(post.Id)
                        };

                        ListPosts.Add(listPost);
                    }
                }
                

            }

            return View(ListPosts);
        }

        [HttpGet]
        public JsonResult AddFollower(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var user = _userService.FindUserById(userId);
            
            var check = _followerService.CheckFollow(userId, id);

            if (check != null)
            {
                return Json(new {message = "NotFound"});
            }

            _followerService.AddFollower(userId, id, DateTime.Now);

            var CreateModel = new CreateNotificationDTO
            {
                Message = $"Blogger {user.FullName} started following you @ {DateTime.Now.ToLongDateString()}",
                UserId = id,
                CreatedBy = user.Email,
                CreatedAt = DateTime.Now,
                Type = "Follow"
            };

            _notificationService.AddNotification(CreateModel);

            return Json(new
            {
                Isfollowing = true
            });
        }


        [HttpGet]
        public IActionResult AddFollowerFromProfile(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var user = _userService.FindUserById(userId);

            var check = _followerService.CheckFollow(userId, id);

            if (check != null)
            {
                return NotFound();
            }

            _followerService.AddFollower(userId, id, DateTime.Now);

            var CreateModel = new CreateNotificationDTO
            {
                Message = $"Blogger {user.FullName} started following you @ {DateTime.Now.ToLongDateString()}",
                UserId = id,
                CreatedBy = user.Email,
                CreatedAt = DateTime.Now,
                Type = "Follow"
            };

            _notificationService.AddNotification(CreateModel);

            return RedirectToAction("UserProfile", new { id = id });
     
        }

        [HttpGet]
        public JsonResult AddFollowerFromListUsers(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var user = _userService.FindUserById(userId);
            
            var check = _followerService.CheckFollow(userId, id);

            if (check != null)
            {
                return Json(new {message = "Unauthorized"});
            }

            _followerService.AddFollower(userId, id, DateTime.Now);

            var CreateModel = new CreateNotificationDTO
            {
                Message = $"Blogger {user.FullName} started following you @ {DateTime.Now.ToLongDateString()}",
                UserId = id,
                CreatedBy = user.Email,
                CreatedAt = DateTime.Now,
                Type = "Follow"
            };

            _notificationService.AddNotification(CreateModel);

            return Json(new
            {
                Isfollowing = true
            });
        }

        [HttpGet]
        public IActionResult Unfollow(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var follow = _followerService.CheckFollow(userId, id);
            if (follow == null)
            {
                return NotFound();
            }

            _followerService.Unfollow(follow.Id);
            return RedirectToAction($"UserProfile", new { id = id });
        }
        
        public IActionResult PendingPostPerUser()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var pendingPosts = _postService.GetPendingPostByUserId(userId);

            var ListPosts = new List<PostDTo>();
           

            foreach (var post in pendingPosts)
            {
                var postsCategory = _postCategoryService.GetCategoryByPostId(post.Id);


                var categories = new List<Category>();

                var listPost = new PostDTo
                {
                    Id = post.Id,
                    PostTitle = post.Title,
                    Content = post.Content,
                    Description = post.Description,
                    CreatedAt = post.CreatedAt,
                    ImageUrl = post.FeaturedImageURL,
                    PostCategories = post.PostCategories.Select(p => p.Category).ToList(),
                };

                ListPosts.Add(listPost);

            }

            return View(ListPosts);

        }

        [Authorize(Roles = "admin")]
        public IActionResult UnApprovedPost()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var pendingPosts = _postService.UnApprovedPost();

            var ListPosts = new List<PostDTo>();


            foreach (var post in pendingPosts)
            {
                var postsCategory = _postCategoryService.GetCategoryByPostId(post.Id);

                var categories = new List<Category>();

                var listPost = new PostDTo
                {
                    Id = post.Id,
                    PostTitle = post.Title,
                    Content = post.Content,
                    Description = post.Description,
                    CreatedAt = post.CreatedAt,
                    ImageUrl = post.FeaturedImageURL,
                    PostCategories = post.PostCategories.Select(p => p.Category).ToList(),
                };

                ListPosts.Add(listPost);

            }

            return View(ListPosts);

        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            var roles = _userRoleService.FindUserRole(userId);

            ViewBag.Role = roles[0].Name;


            ViewBag.name = user.FullName;

            ViewBag.email = user.Email;

            ViewBag.PostId = _postService.FindById(id);

            var post = _postService.FindById(id);
            if (post == null)
            {
                return NotFound();
            }
            var following = false;
            int posterId = _postService.FindById(post.Id).UserId;
            var poster = _userService.FindUserById(posterId);
            var posterRole = _userRoleService.FindUserRole(posterId)[0].Name;
            if (_followerService.CheckFollow(userId, posterId) != null)
            {
                following = true;
            }
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
                PosterRole = posterRole,
                IsFollowing = following,
                PosterFullName = poster.FullName,
                PostCategories = post.PostCategories.Select(p => p.Category).ToList(),
                Like = _reviewService.ReviewCount(post.Id),
                CommentCount = _reviewService.CommentCount(post.Id)
            };
            return View(listPost);


        }


        [HttpPost]
        public IActionResult Detail(PostDTo postVM)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var user = _userService.FindUserById(userId);

            int posterId = _postService.FindById(postVM.Id).UserId;

            var post = _postService.FindById(postVM.Id) ;
            var roles = _userRoleService.FindUserRole(userId);

            ViewBag.Role = roles[0].Name;

            _reviewService.AddComment(userId, postVM.Comment, postVM.Id, user.Email);

            if (userId != posterId)
            {
                var CreateModel = new CreateNotificationDTO
                {
                    Message = $"Blogger {user.FullName} ccommented on your post with Title: '{post.Title}' @ {DateTime.Now.ToLongDateString()}",
                    UserId = _postService.FindById(post.Id).UserId,
                    PostId = post.Id,
                    CreatedBy = user.Email,
                    CreatedAt = DateTime.Now,
                    Type = "Comment",
                };

                _notificationService.AddNotification(CreateModel);
            }

            
            return RedirectToAction("Detail", postVM.Id);
        }

        public IActionResult ListUsers(string sortOrder, string searchString, int? pageNumber)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var roles = _userRoleService.FindUserRole(userId);

            ViewBag.Role = roles[0].Name;

            User user = _userService.FindUserById(userId);

            ViewBag.name = user.FullName;

            ViewBag.email = user.Email;

            List<ListUserVM> ListUsers = new List<ListUserVM>();

            var users = _userService.GetUsers(userId);
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.FullName.Contains(searchString) || s.Email.Contains(searchString));
            }


            foreach (var userL in users)
            {

                var following = false;

                if (_followerService.CheckFollow(userId, userL.Id) != null)
                {
                    following = true;
                }


                var ListUser = new ListUserVM
                {
                    Id = userL.Id,
                    FullName = userL.FullName,
                    Email = userL.Email,
                    IsFollowing = following,
                };

                ListUsers.Add(ListUser);
            }

            int pageSize = 5;
            return View(PaginatedList<ListUserVM>.CreateAsync(ListUsers.AsQueryable(), pageNumber ?? 1, pageSize));

        }

        [HttpGet]
        public IActionResult UserProfile(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var roles = _userRoleService.FindUserRole(userId);

            ViewBag.Role = roles[0].Name;


            var userLog = _userService.FindUserById(userId);

            var user = _userService.FindUserById(id);

            ViewBag.name = userLog.FullName;

            ViewBag.email = userLog.Email;

            List<Post> userPosts = _postService.GetPostByUserId(id);

            var ListPosts = new List<PostDTo>();

            foreach (var post in userPosts)
            {
                var postsCategory = _postCategoryService.GetCategoryByPostId(post.Id);

                var categories = new List<Category>();
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
                    PosterFullName = poster.FullName,
                    PostCategories = post.PostCategories.Select(p => p.Category).ToList(),
                    Like = _reviewService.ReviewCount(post.Id),
                    CommentCount = _reviewService.CommentCount(post.Id)
                };

                ListPosts.Add(listPost);
            }

            var following = false || _followerService.CheckFollow(userId, id) != null;


            UserProfileVM userProfile = new UserProfileVM
            {
                Fullname = user.FullName,
                Followers = _followerService.GetFollowersOfUser(id).Count(),
                Following = _followerService.GetFollowingOfUser(id).Count(),
                ListOfPosts = ListPosts,
                UserId = id,
                IsFollowing = following,
                UserLogEmail = user.Email,
                NoOfPosts = ListPosts.Count(),
            };

            return View(userProfile);
        }


        [HttpGet]
        public IActionResult DetailAnonymous(int id)
        {
           
            var post = _postService.FindById(id);
            if (post == null)
            {
                return NotFound();
            }
            
            int posterId = _postService.FindById(post.Id).UserId;
            var poster = _userService.FindUserById(posterId);
            IEnumerable<PostCategory> postCategories = null;
            List<Post> PostPerCategory = new List<Post>();
            if (_postCategoryService.GetCategoryByPostId(id) != null)
            {
                postCategories = _postCategoryService.GetCategoryByPostId(id);
                var postCategories2 = _postCategoryService.GetOverallPostCategories();
                var postCategory = postCategories.ToList()[0];
                var posts = _postService.ApprovedPost();
                foreach(var pg in postCategories2)
                {
                    if(postCategory.Category.Id == pg.CategoryId)
                    {
                        var postR = _postService.FindById(pg.PostId);
                        if (postR.Status == true  && postR.Id != post.Id)
                        {
                            PostPerCategory.Add(postR);
                        }
                    }
                }
            }
           

            var listPost = new PostDTo
            {
                Id = post.Id,
                PostTitle = post.Title,
                Content = post.Content,
                Description = post.Description,
                PostUrl = post.PostURL,
                CreatedAt = post.CreatedAt,
                CurrentTime = post.CreatedAt - DateTime.Now,
                CreatedBy = post.CreatedBy,
                ImageUrl = post.FeaturedImageURL,
                Status = post.Status,
                Created = posterId,
                PosterFullName = poster.FullName,
                PostPerCategories = PostPerCategory,
            };
            return View(listPost);


        }
    }
}


