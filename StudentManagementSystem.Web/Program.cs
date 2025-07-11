////using Microsoft.AspNetCore.Authorization;
////using Microsoft.AspNetCore.Identity;
////using Microsoft.AspNetCore.Mvc.Authorization;
////using Microsoft.EntityFrameworkCore;
////using StudentManagementSystem.Business.Interfaces;
////using StudentManagementSystem.Business.Services;
////using StudentManagementSystem.DataAccess.Data;
////using StudentManagementSystem.DataAccess.Interfaces;
////using StudentManagementSystem.DataAccess.Repositories;
////using StudentManagementSystem.Entities.Entity;

////var builder = WebApplication.CreateBuilder(args);
////builder.Services.AddDbContext<ApplicationDbContext>(options =>
////    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
////builder.Services.AddIdentity<IdentityUser, IdentityRole>()
////    .AddEntityFrameworkStores<ApplicationDbContext>()
////    .AddDefaultTokenProviders();
////builder.Services.AddScoped<IUserService, UserService>();
////builder.Services.AddScoped<IStudentService, StudentService>();
////builder.Services.AddScoped<IStudentRepository, StudentRepository>();
////builder.Services.AddScoped<ICourseService, CourseService>();
////builder.Services.AddScoped<ICourseRepository, CourseRepository>();
////builder.Services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
////builder.Services.AddScoped<IStudentCourseService, StudentCourseService>();






////builder.Services.AddControllersWithViews(options =>
////{
////    var policy = new AuthorizationPolicyBuilder()
////        .RequireAuthenticatedUser()
////        .Build();
////    options.Filters.Add(new AuthorizeFilter(policy));
////});
////builder.Services.AddSession();
////var app = builder.Build();
////using (var scope = app.Services.CreateScope())
////{
////    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


////    var roles = new[] { "SuperAdmin", "Admin", "Student" };
////    foreach (var roleName in roles)
////    {
////        if (!db.Roles.Any(r => r.Name == roleName))
////        {
////            db.Roles.Add(new Role { Name = roleName });
////        }
////    }

////    await db.SaveChangesAsync();


////    var superAdminEmail = "superadmin@domain.com";
////    var superAdmin = db.Users.FirstOrDefault(u => u.Email == superAdminEmail);
////    if (superAdmin == null)
////    {
////        var superAdminRoleId = db.Roles.First(r => r.Name == "SuperAdmin").Id;
////        db.Users.Add(new User
////        {
////            Email = superAdminEmail,
////            Password = "passwordss",
////            RoleId = superAdminRoleId
////        });
////        await db.SaveChangesAsync();
////    }
////}


////// Configure the HTTP request pipeline.
////if (!app.Environment.IsDevelopment())
////{
////    app.UseExceptionHandler("/Home/Error");
////    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
////    app.UseHsts();
////}

////app.UseSession();
//////app.UseHttpsRedirection();
////app.UseStaticFiles();

////app.UseRouting();

////app.UseAuthorization();

////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller=User}/{action=Login}/{id?}");


////app.Run();
//// Program.cs
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc.Authorization;
//using Microsoft.EntityFrameworkCore;
//using StudentManagementSystem.Business.Interfaces;
//using StudentManagementSystem.Business.Mapping;
//using StudentManagementSystem.Business.Services;
//using StudentManagementSystem.DataAccess.Data;
//using StudentManagementSystem.DataAccess.Interfaces;
//using StudentManagementSystem.DataAccess.Repositories;
//using StudentManagementSystem.Entities.Entity;

//var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

////builder.Services.AddIdentity<IdentityUser, IdentityRole>()
////    .AddEntityFrameworkStores<ApplicationDbContext>()
////    .AddDefaultTokenProviders();

//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IStudentService, StudentService>();
//builder.Services.AddScoped<IStudentRepository, StudentRepository>();
//builder.Services.AddScoped<ICourseService, CourseService>();
//builder.Services.AddScoped<ICourseRepository, CourseRepository>();
//builder.Services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
//builder.Services.AddScoped<IStudentCourseService, StudentCourseService>();
//builder.Services.AddAutoMapper(typeof(MappingProfile));

////builder.Services.AddHttpContextAccessor();
//builder.Services.AddControllersWithViews(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});

//builder.Services.AddSession();

//var app = builder.Build();


//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    var roles = new[] { "SuperAdmin", "Admin", "Student" };
//    foreach (var roleName in roles)
//    {
//        if (!db.Roles.Any(r => r.Name == roleName))
//        {
//            db.Roles.Add(new Role { Id = Guid.NewGuid().ToString(), Name = roleName });
//        }
//    }

//    await db.SaveChangesAsync();

//    var superAdminEmail = "superadmin@domain.com";
//    var superAdmin = db.Users.FirstOrDefault(u => u.Email == superAdminEmail);
//    if (superAdmin == null)
//    {
//        var superAdminRoleId = db.Roles.First(r => r.Name == "SuperAdmin").Id;
//        db.Users.Add(new User
//        {
//            //Id = Guid.NewGuid().ToString(),
//            Email = superAdminEmail,
//            Password = "password", // consider hashing later
//            RoleId = superAdminRoleId
//        });

//        await db.SaveChangesAsync();
//    }
//}


//// Middleware
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseSession();
////app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=User}/{action=Login}/{id?}");

//app.Run();


using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.Business.Mapping;
using StudentManagementSystem.Business.Services;
using StudentManagementSystem.DataAccess.Data;
using StudentManagementSystem.DataAccess.Interfaces;
using StudentManagementSystem.DataAccess.Repositories;
using StudentManagementSystem.Entities.Entity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register services and repositories
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
builder.Services.AddScoped<IStudentCourseService, StudentCourseService>();

builder.Services.AddScoped<IAttendanceeRepository, AttendanceeRepository>();
builder.Services.AddScoped<IAttendanceeService, AttendanceeService>();

builder.Services.AddScoped<IInstructorRepository, InstructorRepository>(); // ✅ This is what fixes the error
builder.Services.AddScoped<IInstructorService, InstructorService>();


// MVC with global auth filter (if needed)
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSession(); // already needed for HttpContext.Session
builder.Services.AddHttpContextAccessor(); // optional if not added yet
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = "/User/AccessDenied";
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddRazorPages();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

   
    var roles = new[] { "SuperAdmin", "Admin", "Student" };
    foreach (var roleName in roles)
    {
        if (!db.Roles.Any(r => r.Name == roleName))
        {
            db.Roles.Add(new Role {  Name = roleName });
        }
    }
    await db.SaveChangesAsync();

    
    var superAdminEmail = "superadminAccess@domain.com";
    var superAdmin = db.Users.FirstOrDefault(u => u.Email == superAdminEmail);

    if (superAdmin == null)
    {
        var superAdminRole = db.Roles.FirstOrDefault(r => r.Name == "SuperAdmin");

        if (superAdminRole != null)
        {
            db.Users.Add(new User
            {
                Name = "Super Admin",
                Email = superAdminEmail,
                Password = "Super123", 
                RoleId = superAdminRole.Id
            });
            await db.SaveChangesAsync();
        }
    }
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();

//app.UseHttpsRedirection(); 
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");
app.MapControllers();
app.Run();
