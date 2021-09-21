using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Post AddBlogPost(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return post;
        }

        public void Delete(int id)
        {
            var post = FindById(id);
            {
                if (post != null)
                {
                    _dbContext.Posts.Remove(post);
                    _dbContext.SaveChanges();
                }
            }
        }

        public Post FindById(int? id)
        {
            return _dbContext.Posts.Include(u=>u.User).FirstOrDefault(p=> p.Id == id);
        }

        public User FindByUser(int userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public List<Post> GetPostByUserId(int userId)
        {
            return _dbContext.Posts.Where(r => r.UserId == userId).OrderByDescending(r=> r.CreatedAt).ToList();
        }

        public List<Post> GetPendingPostByUserId(int userId)
        {
            return _dbContext.Posts.Include(u=> u.User).Where(r => r.User.Id == userId && r.Status == false).OrderByDescending(r => r.CreatedAt).ToList();
        }

        public List<PostCategory> GetAllPostCategories(int postId)
        {
            return _dbContext.PostCategories.Where(post => post.PostId == postId).ToList();
        }

        public IEnumerable<Post> UnApprovedPost()
        {
            return _dbContext.Posts.Where(post => post.Status == false).OrderByDescending(p => p.CreatedAt).ToList();
        }

        public IEnumerable<Post> ApprovedPost()
        {
            return _dbContext.Posts.Include(post => post.Reviews).Include(post => post.PostCategories).ThenInclude(post => post.Category).Where(post => post.Status == true).OrderByDescending(p => p.CreatedAt).ToList();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _dbContext.Posts.Include(p => p.Reviews).Include(p => p.PostCategories).ThenInclude(p => p.Category).OrderByDescending(p => p.CreatedAt).ToList();
        }

        public IEnumerable<Review> GetAllPostReviews(int postId)
        {
            return _dbContext.Reviews.Where(review => review.PostId == postId).ToList();
        }

        public Post UpdatePost(Post post)
        {
            _dbContext.Posts.Update(post);
            _dbContext.SaveChanges();
            return post;
        }

        public IList<Post> Search(string searchText) 
        {
            return _dbContext.Posts.Where(post => EF.Functions.Like(post.Content, $"%{searchText}%")).ToList();
        }
    }
}
