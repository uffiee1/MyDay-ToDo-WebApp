using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyDayApp.BusinessLogic.AccountLogic;
using MyDayApp.BusinessLogic.AccountLogic.Interfaces;
using MyDayApp.BusinessLogic.ToDoLogic;
using MyDayApp.BusinessLogic.ToDoLogic.Interfaces;
using MyDayApp.BusinessLogic.AdminLogic;
using MyDayApp.BusinessLogic.AdminLogic.Interfaces;
using MyDayApp.DataAccess;
using MyDayApp.Models;
using IdentityOptions = Microsoft.AspNetCore.Identity.IdentityOptions;

namespace MyDayApp
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
            services.AddScoped<AppDbContext>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer
                (Configuration.GetConnectionString("MyDay")));

            //This is for a Identity Check.
            //services.AddScoped<SignInManager<User>, SignInManager<User>>();
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            //Add Minimal Requirements
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = false;
            });

            //Add Cookie options
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Account/Login";
                options.AccessDeniedPath = "/User/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            });

            //Logic
            services.AddScoped<ILoginLogic, LoginLogic>();
            services.AddScoped<ILogoutLogic, LogoutLogic>();
            services.AddScoped<IRegisterLogic, RegisterLogic>();
            services.AddScoped<IRoleLogic, RoleLogic>();
            services.AddScoped<IToDoLogic, ToDoLogic>();
            services.AddScoped<ILogger, Logger<ToDo>>();

            //This is for a Identity Check.
            //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
