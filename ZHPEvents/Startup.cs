﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZHPEvents.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using ZHPEvents.Areas.Identity.Services;
using ZHPEvents.Models.Identity;

namespace ZHPEvents
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ZHPEventsUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                //Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ZHPEventsUser>>();
            string[] roleNames = { "Administrator", "Editor", "Author", "EventEditor", "EventAuthor", "RaportEditor", "RaportAuthor", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            ZHPEventsUser administrator = await UserManager.FindByEmailAsync("administrator@gmail.com");

            if (administrator == null)
            {
                administrator = new ZHPEventsUser()
                {
                    UserName = "administrator@gmail.com",
                    Email = "administrator@gmail.com",
                    EmailConfirmed = true,
                };
                await UserManager.CreateAsync(administrator, "Administrator!1");
            }
            await UserManager.AddToRoleAsync(administrator, "Administrator");

            ZHPEventsUser editor = await UserManager.FindByEmailAsync("editor@gmail.com");

            if (editor == null)
            {
                editor = new ZHPEventsUser()
                {
                    UserName = "editor@gmail.com",
                    Email = "editor@gmail.com",
                    EmailConfirmed = true,
                };
                await UserManager.CreateAsync(editor, "Editor!1");
            }
            await UserManager.AddToRoleAsync(editor, "Editor");

            ZHPEventsUser author = await UserManager.FindByEmailAsync("author@gmail.com");

            if (author == null)
            {
                author = new ZHPEventsUser()
                {
                    UserName = "author@gmail.com",
                    Email = "author@gmail.com",
                    EmailConfirmed = true,
                };
                await UserManager.CreateAsync(author, "Author!1");
            }
            await UserManager.AddToRoleAsync(author, "Author");

            ZHPEventsUser eventEditor = await UserManager.FindByEmailAsync("eventEditor@gmail.com");

            if (eventEditor == null)
            {
                eventEditor = new ZHPEventsUser()
                {
                    UserName = "eventEditor@gmail.com",
                    Email = "eventEditor@gmail.com",
                    EmailConfirmed = true,
                };
                await UserManager.CreateAsync(eventEditor, "EventEditor!1");
            }
            await UserManager.AddToRoleAsync(eventEditor, "EventEditor");

            ZHPEventsUser eventAuthor = await UserManager.FindByEmailAsync("eventAuthor@gmail.com");

            if (eventAuthor == null)
            {
                eventAuthor = new ZHPEventsUser()
                {
                    UserName = "eventAuthor@gmail.com",
                    Email = "eventAuthor@gmail.com",
                    EmailConfirmed = true,
                };
                await UserManager.CreateAsync(eventAuthor, "EventAuthor!1");
            }
            await UserManager.AddToRoleAsync(eventAuthor, "EventAuthor");

            ZHPEventsUser raportEditor = await UserManager.FindByEmailAsync("raportEditor@gmail.com");

            if (raportEditor == null)
            {
                raportEditor = new ZHPEventsUser()
                {
                    UserName = "raportEditor@gmail.com",
                    Email = "raportEditor@gmail.com",
                    EmailConfirmed = true,
                };
                await UserManager.CreateAsync(raportEditor, "RaportEditor!1");
            }
            await UserManager.AddToRoleAsync(raportEditor, "RaportEditor");

            ZHPEventsUser raportAuthor = await UserManager.FindByEmailAsync("raportAuthor@gmail.com");

            if (raportAuthor == null)
            {
                raportAuthor = new ZHPEventsUser()
                {
                    UserName = "raportAuthor@gmail.com",
                    Email = "raportAuthor@gmail.com",
                    EmailConfirmed = true,
                };
                await UserManager.CreateAsync(raportAuthor, "RaportAuthor!1");
            }
            await UserManager.AddToRoleAsync(raportAuthor, "RaportAuthor");

            ZHPEventsUser user = await UserManager.FindByEmailAsync("user@gmail.com");

            if (user == null)
            {
                user = new ZHPEventsUser()
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                    EmailConfirmed = true,
                };
                await UserManager.CreateAsync(user, "User!1");
            }
            await UserManager.AddToRoleAsync(user, "User");
        }
    }
}
