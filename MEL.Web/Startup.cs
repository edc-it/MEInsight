using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MEL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MEL.Entities.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Rewrite;

namespace MEL.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment CurrentEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            // Add Entity Framework Core (EF Core) default DbContext using SqlServer database provider
            // the DefaultConnection string is set in appsettings.json
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Add ASP.NET Core Identity system for Users and Roles
            // Customized user and role data in ApplicationUser and ApplicationRole
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Update the limit for the number of form entries to allow
            services.Configure<FormOptions>(x => x.ValueCountLimit = 4096);

            // Add Localization (add Resources folder)
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Session State - for storage of user data while the user browses the app
            // Setup for in-memory session provider
            services.AddDistributedMemoryCache();
            // Sets Idle Session Timeout to 2 hours (before requesting user to login again)
            services.AddSession(options =>
            {
                options.Cookie.Name = ".MEInsight.Session";
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add MVC services with View and Data Annotation Localization options
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(typeof(SharedResource));
                    };

                })
                // Add TempData provider
                .AddSessionStateTempDataProvider()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Configure supported cultures and localization options
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("en-ZA"),
                    new CultureInfo("fr-ML"),
                    new CultureInfo("es-HN")
                };

                // Default supported culture
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

                // Cultures supported.
                // These cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings.
                options.SupportedUICultures = supportedCultures;

            });

            // Add CookieTempDataProvider for ViewData Messaging
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            // Using Role-based authorization to validate user roles access
            // a member of a higher security role will inherit lower security policies.
            // an Administrator will be able to Create, Update, Delete
            // Roles are seeded through ApplicationDbInitializer.cs
            services.AddAuthorization(options =>
            {
                // Role-based policies
                // Allow create records only
                options.AddPolicy("RequireCreateRole",
                    policy => policy.RequireRole("Create", "Edit", "Delete", "MELOfficer", "MEL", "Administrator"));

                // Allow create and update records
                options.AddPolicy("RequireEditRole",
                    policy => policy.RequireRole("Edit", "Delete", "MELOfficer", "MEL", "Administrator"));

                // Allow create, update, delete records
                options.AddPolicy("RequireDeleteRole",
                    policy => policy.RequireRole("Delete", "MELOfficer", "MEL", "Administrator"));

                // Allow access to M&E only areas for Monitoring, Evaluation and Learning (MEL) Officer access
                // Allow create, update, delete, and M&E Officer access
                options.AddPolicy("RequireMELOfficerRole",
                    policy => policy.RequireRole("MELOfficer", "MEL", "Administrator"));

                // Allow Monitoring, Evaluation and Learning (MEL) admin access
                // Allow create, update, delete, and M&E Officer, and M&E Admin access
                options.AddPolicy("RequireMELRole",
                    policy => policy.RequireRole("MEL", "Administrator"));

                // Administrator default role
                options.AddPolicy("RequireAdministratorRole",
                    policy => policy.RequireRole("Administrator"));
            });

            // Configures Identity options for password requirements and session lockout
            // These settings require for a secure password to include 1 digit, 1 uppercase and 8 characters (Does not require symbols)
            // Lockout TimeSpan is set to 24 hours
            // Email is the unique Username
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(24);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User settings
                // options.User.RequireUniqueEmail = true;
            });

            // Require HTTPS in all pages in Production mode
            if (CurrentEnvironment.IsProduction())
            {
                services.Configure<MvcOptions>(options =>
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                });
            }

        }

        // Runtime configure
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            // Add Localization Options
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            // If environment is Development
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            // if environment is Production
            else
            {
                app.UseExceptionHandler("/Home/Error");

                //Adds Redirect from HTTP to HTTPS rewrite
                var options = new RewriteOptions()
                    .AddRedirectToHttps();

                app.UseRewriter(options);

                app.UseHsts();

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}