using MarvinBlogv._2._0.DTO;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Models;
using MarvinBlogv._2._0.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarvinBlogv._2._0.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IUserRoleService userRoleService, IRoleService roleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CreateUserViewModel model) 
        {

            var CreateModel = new CreateUserDto
            {
                ConfirmPassword = model.ConfirmPassword,
                Email = model.Email,
                FullName = model.FullName,
                Id = model.Id,
                Password = model.Password,
                CreatedAt = DateTime.Now,
            };
            _userService.RegisterUser(CreateModel);

            ViewBag.Message = "Successfully Registered, You can now Log In";
            return RedirectToAction("Login");
            
        }

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    //if(HttpContext.User.Identity.IsAuthenticated) 
        //    //{
        //    //    var routeName = HttpContext.Request.Path;

        //    //    return View();
        //    //}
        //    return View();
        //}

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogoutOption");
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            User user = _userService.LoginUser(model.Email, model.Password);

            if (user == null)
            {
                return View();
            }

            if(model.Password != user.PasswordHash) 
            {
                ViewBag.Message = "Access Denied";
            }

            var roles = _userRoleService.FindUserRole(user.Id);

            var role = roles[0].Name;


            if(role == "xx") 
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, "SuperAdmin"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                return RedirectToAction("Index", "SuperAdmin");
            }

            else if(role == "SuperAdmin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, "admin"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                return RedirectToAction("UnApprovedPost", "Blogger");
            }

            else if (role == "blogger")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, "blogger"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                return RedirectToAction("Index", "Blogger");
            }

            return NotFound();
        }

        public IActionResult LogoutOption()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
