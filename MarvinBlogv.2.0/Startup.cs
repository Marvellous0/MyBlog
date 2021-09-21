using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvinBlogv._2._0.Context;
using MarvinBlogv._2._0.Interfaces;
using MarvinBlogv._2._0.Repositories;
using MarvinBlogv._2._0.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarvinBlogv._2._0
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
     
            services.AddDbContext<BlogDbContext>(option => option.UseMySql(Configuration.GetConnectionString("BlogConnectionString")));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<IPostService, PostService>();

            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddScoped<INotificationService, NotificationService>();


            services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();

            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<IReviewService, ReviewService>();


            services.AddScoped<IPostCategoryService, PostCategoryService>();

            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            
            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped<IFollowerService, FollowerService>();

            services.AddScoped<IFollowerRepository, FollowerRepository>();
            

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
            {
                config.LoginPath = "/User/Login";
       
                config.Cookie.Name = "SimpleUser";
            });
            
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
