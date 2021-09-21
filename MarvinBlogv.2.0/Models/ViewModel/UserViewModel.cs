using MarvinBlogv._2._0.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MarvinBlogv._2._0.DTO;

namespace MarvinBlogv._2._0.Models.ViewModel
{
    public class UserViewModel
    {
    }

    public class CreateUserViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "Full Name:")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "User E-mail is required")]
        [Display(Name = "E-mail Address:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password!!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "User E-mail is required")]
        [Display(Name = "E-mail Address:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Password is required")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }

    public class ListUserVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsFollowing { get; set; }
    }

    public class UserProfileVM
    {
        public string Fullname { get; set; }

        public int Followers { get; set; }

        public int Following { get; set; }

        public int NoOfPosts { get; set; }

        public int UserId { get; set; }

        public bool IsFollowing { get; set; }

        public string UserLogEmail { get; set; }

       public List<PostDTo> ListOfPosts { get; set; }
    }

}
