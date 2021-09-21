using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using MarvinBlogv._2._0.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Renci.SshNet.Messages;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace MarvinBlogv._2._0.Controllers
{
    public class ReviewController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IPostService _postService;
        private readonly IUserRoleService _userRoleService;
        private readonly IReviewService _reviewService;
        private readonly IFollowerService _followerService;

        public ReviewController(IUserService userService, IReviewService reviewService, IPostService postService, IFollowerService followerService, IUserRoleService userRoleService, INotificationService notificationService)
        {
            _userService = userService;
            _reviewService = reviewService;
            _postService = postService;
            _followerService = followerService;
            _userRoleService = userRoleService;
            _notificationService = notificationService;
        }
        
        [HttpGet]
        public  JsonResult AddLike(int id)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated) return Json(new {message= "Unauthorized"});
            
            ViewBag.Likes = _reviewService.ReviewCount(id);
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            string email = User.FindFirst(ClaimTypes.Email).Value;

            string fullName = User.FindFirst(ClaimTypes.Name).Value;

            var review = _reviewService.CheckLike(id, userId);
            
            var post = _postService.FindById(id);

            int posterId = _postService.FindById(post.Id).UserId;
         

            if (review != null)
            {
                _reviewService.Delete(review.Id);
                return Json(new
                {
                    isLiked = false,
                    Likes = _reviewService.ReviewCount(post.Id)
                });
            }
            _reviewService.AddReview(userId, true, id, email);

            
            if(posterId != userId)
            {
                var CreateModel = new CreateNotificationDTO
                {
                    Message = $"{fullName} liked your post with Title: '{post.Title}' @ {DateTime.Now.ToLongDateString()}",
                    UserId = posterId,
                    PostId = id,
                    CreatedBy = email,
                    CreatedAt = DateTime.Now,
                    Type = "Like"
                };

                _notificationService.AddNotification(CreateModel);
            }

            return Json(new
            {
                isLiked = true,
                Likes = _reviewService.ReviewCount(post.Id)
            });
        }

       
        [Authorize(Roles = "admin, blogger")]
        public IActionResult AddComment()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, blogger")]
        public IActionResult AddComment(CreateReviewViewModel model)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            var post = _postService.FindById(model.Id);

            int posterId = _postService.FindById(post.Id).UserId;

            var poster = _userService.FindUserById(posterId);

            var comment = _reviewService.FindReviewById(userId);

            _reviewService.AddComment(userId, model.Comment, model.Id, user.Email);

            if (posterId != userId)
            {
                var CreateModel = new CreateNotificationDTO
                {
                    Message = $"Blogger {user.FullName} commented on your post with Title: '{post.Title}' @ {comment.CreatedAt.ToLongDateString()}",
                    UserId = posterId,
                    PostId = model.Id,
                    CreatedBy = user.Email,
                    CreatedAt = DateTime.Now,
                    Type = "Comment"
                };

                _notificationService.AddNotification(CreateModel);
            }
            
            return RedirectToAction("Index", "Blogger");
        }


        public IActionResult LikeList(int id)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            var roles = _userRoleService.FindUserRole(userId);

            ViewBag.Role = roles[0].Name;


            ViewBag.name = user.FullName;
            ViewBag.PostId = id;
            ViewBag.email = user.Email;
            List<LikeList> likeLists = new List<LikeList>();
            var reviews = _reviewService.FindByPostId(id);
            foreach(var review in reviews)
            {
                var posterRole = _userRoleService.FindUserRole(review.UserId)[0].Name;
                bool following = false;
                if (_followerService.CheckFollow(userId, review.UserId) != null)
                {
                    following = true;
                }
                var likeList = new LikeList
                {
                    FullName = _userService.FindUserById(review.UserId).FullName,
                    UserId = _userService.FindUserById(review.UserId).Id,
                    CreatedAt = review.CreatedAt,
                    PosterRole = posterRole,
                    IsFollowing = following,
                };

                likeLists.Add(likeList);
            }
            return View(likeLists);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetReviewList(int id)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var roles = _userRoleService.FindUserRole(userId);

            User user = _userService.FindUserById(userId);

            ViewBag.PostId = id;

            ViewBag.Role = roles[0].Name;

            ViewBag.name = user.FullName;

            var post = _postService.FindById(id);
            ViewBag.UserId = userId;
            List<CommentList> CommentList = new List<CommentList>();
            var comments = _reviewService.GetAllComments(id); ;

            foreach (var comment in comments)
            {
                var posterRole = _userRoleService.FindUserRole(comment.UserId)[0].Name;
                CommentList commentList = new CommentList
                {
                    Id = comment.Id,
                    UserId = comment.UserId,
                    FullName = _userService.FindUserById(comment.UserId).FullName,
                    Comment = comment.Comment,
                    CreatedAt = comment.CreatedAt,
                    PosterRole = posterRole,
                    CommentCount = _reviewService.CommentCount(post.Id),
                };
                CommentList.Add(commentList);
            }

            return View(CommentList);
        }

        public IActionResult DeleteComment(int id) 
        {
            var comment =  _reviewService.FindReviewById(id);
            if(comment == null)
            {
                return NotFound();
            }
            _reviewService.Delete(comment.Id);

            return RedirectToAction("GetReviewList", new { id = comment.PostId });
        }
    }
}
