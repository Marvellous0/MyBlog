using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Services
{
    public class PostService : IPostService
    {
        private readonly IUserService _userService;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostCategoryRepository _postCategoryRepository;
        public PostService(IUserService userService, IPostRepository postRepository, IPostCategoryRepository postCategoryRepository, ICategoryRepository categoryRepository)
        {
            _userService = userService;
            _postRepository = postRepository;
            _postCategoryRepository = postCategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public void AddBlogPost(int id, DateTime publishedOn, string name, string title, string featuredImageURL, string content, string description, string postURL, int userId, string[] categoryIds, string createdBy, bool status)
        {
            if(categoryIds.Length != 0)
            {
                var postCategories = new List<PostCategory>() { };

                foreach (var catId in categoryIds)
                {
                    var postCat = new PostCategory
                    {
                        PostId = id,
                        CategoryId = int.Parse(catId),
                    };
                    postCategories.Add(postCat);
                }

                Post post = new()
                {
                    Id = id,
                    CreatedAt = DateTime.Now,
                    Title = title.ToUpper(),
                    FeaturedImageURL = featuredImageURL,
                    Content = content,
                    PostCategories = postCategories,                  
                    PostURL = Guid.NewGuid().ToString().Substring(0, 7),
                    Description = description,
                    UserId = _userService.FindUserById(userId).Id,
                    CreatedBy = _userService.FindUserById(userId).Email,
                    Status = status
                };

                _postRepository.AddBlogPost(post);
            }
            
        }

        public void Delete(int id)
        {
            _postRepository.Delete(id);
        }

        public Post FindById(int? id)
        {
            return _postRepository.FindById(id);
        }

        public User FindByUser(int userId)
        {
            return _userService.FindUserById(userId);
        }

        public List<PostCategory> GetAllPostCategories(int postId)
        {
            var postCategory = _postRepository.GetAllPostCategories(postId);
            return postCategory;
        }

        public IEnumerable<Review> GetAllPostReviews(int postId)
        {
            var review = _postRepository.GetAllPostReviews(postId);
            return review;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _postRepository.GetAllPosts().Select(p => new Post
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title.ToUpper(),
                Content = p.Content,
                Description = p.Description,
                FeaturedImageURL = p.FeaturedImageURL,
                CreatedBy = p.CreatedBy,
                PostCategories = p.PostCategories,
                Reviews = p.Reviews,
                PostURL = p.PostURL,
                Status = p.Status
            }).ToList();

            return posts;
        }

        public List<Post> ApprovedPost()
        {
            var posts = _postRepository.ApprovedPost().Select(p => new Post
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title,
                Content = p.Content,
                FeaturedImageURL = p.FeaturedImageURL,
                CreatedBy = p.CreatedBy,
                PostCategories = p.PostCategories,
                Reviews = p.Reviews,
                Description = p.Description,
                PostURL = p.PostURL,
                Status = p.Status
            }
            ).ToList();
            return posts;
        }

        public IEnumerable<Post> UnApprovedPost() 
        {
            var posts = _postRepository.UnApprovedPost().Select(p => new Post
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title,
                Content = p.Content,
                FeaturedImageURL = p.FeaturedImageURL,
                CreatedBy = p.CreatedBy,
                Description = p.Description,
                PostURL = p.PostURL,
                Status = p.Status
            }
           ).ToList();
            return posts;
        }

        public Post UpdatePost(int id, DateTime createdAt, string title, string featuredImageURL, string content, ICollection<PostCategory> categoryIds, string description, string postURL, bool status)
        {
            var post = _postRepository.FindById(id);

            post.CreatedAt = DateTime.Now;

            post.Title = title.ToUpper();

            post.FeaturedImageURL = featuredImageURL;

            post.Content = content;    

            post.PostCategories = categoryIds;

            post.Description =description;

            post.PostURL = postURL;

            post.Status = status;

            return _postRepository.UpdatePost(post);
        }

        public List<Post> GetPostByUserId(int userId)
        {
            return _postRepository.GetPostByUserId(userId);
        }

        public IEnumerable<Post> GetPendingPostByUserId(int userId)
        {
            return _postRepository.GetPendingPostByUserId(userId);
        }
    }
}
