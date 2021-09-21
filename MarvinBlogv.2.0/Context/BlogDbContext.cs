using MarvinBlogv._2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Context
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(c => c.AssociatedPosts)
                .WithOne(cr => cr.Category)
                .HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>().HasMany(p => p.PostCategories)
                .WithOne(p => p.Post)
                .HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(u => u.userRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>().HasMany(u => u.userRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Email, u.FullName })
                .IsUnique(true);
            
            modelBuilder.Entity<Post>()
                .Property(e => e.Content)
                .HasColumnType("LONGTEXT");

            modelBuilder.Entity<Post>()
                .HasIndex(p => new { p.Title });

            modelBuilder.Entity<Post>()
               .HasIndex(p => new { p.Description });
            
            //modelBuilder.Entity<Post>()
            //    .HasIndex(p => new { p.Content })
            //   .IsUnique(true);


            modelBuilder.Entity<Post>()
               .HasIndex(p => new { p.PostURL })
               .IsUnique(true);

            modelBuilder.Entity<Category>()
                .HasIndex(c => new { c.Name })
                .IsUnique(true);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Marvellous Adeoye",
                    Email = "adeoyemarvellous7@gmail.com",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "adeoyemarvellous7@gmail.com",
                    PasswordHash = "jeYMxCrAXGBEfEJB7j3IuPv4LhgThc7OIsAovL/J13Q=",
                    HashSalt = "GHAku+jJgJVENsz/Y7le9w==",
                }
            );
            
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 2,
                    Name = "admin",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "adeoyemarvellous7@gmail.com",
                }
            );
                        
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 3,
                    Name = "blogger",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "adeoyemarvellous7@gmail.com",
                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "SuperAdmin",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "adeoyemarvellous7@gmail.com",
                }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    UserId = 1,
                    RoleId = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "adeoyemarvellous@gmail.com",
                }
            );
        }
    }
}
