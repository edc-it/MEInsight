using MEInsight.Web.Data;
using MEInsight.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Razor;
using MEInsight.Web;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Replace for using PostgreSQL instead of Microsoft SQL
//   options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add ASP.NET Core Identity system for Users and Roles
// Customized user and role data in ApplicationUser and ApplicationRole
builder.Services
    .AddDefaultIdentity<ApplicationUser>
        (options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<ApplicationRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Update the limit for the number of form entries to allow
builder.Services.Configure<FormOptions>(x => x.ValueCountLimit = 4096);

// Add Localization (add Resources folder)
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            return factory.Create(typeof(SharedResource));
        };

    })
    // Add TempData provider
    .AddSessionStateTempDataProvider();

// Session State - for storage of user data while the user browses the app
// Setup for in-memory session provider
builder.Services.AddDistributedMemoryCache();

// Sets Idle Session Timeout to 2 hours (before requesting user to login again)
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "MEInsightSession";
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "MEInsightApp";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    //options.LoginPath = "/Identity/Account/Login";
    // ReturnUrlParameter requires 
    // add using Microsoft.AspNetCore.Authentication.Cookies;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

// Configure supported cultures and localization options
builder.Services.Configure<RequestLocalizationOptions>(options =>
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
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

// Using Role-based authorization to validate user roles access
// a member of a higher security role will inherit lower security policies.
// an Administrator will be able to Create, Update, Delete
// Roles are seeded through ApplicationDbInitializer.cs
builder.Services.AddAuthorization(options =>
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
// These default settings require for a secure password to include 1 digit, 1 uppercase, 1 symbol, and 8 characters length min
// Lockout TimeSpan is set to 24 hours
// Email is the unique Username
builder.Services.Configure<IdentityOptions>(options =>
{
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;

    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// Add Localization Options
var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (locOptions != null)
{
    app.UseRequestLocalization(locOptions.Value);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseSession();

app.MapAreaControllerRoute(
    name: "SettingsArea",
    areaName: "Settings",
    pattern: "Settings/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
