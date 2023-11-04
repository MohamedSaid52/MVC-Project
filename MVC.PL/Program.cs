using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVC.BLL.Interfaces;
using MVC.BLL.Repositories;
using MVC.DAL.Contexts;
using MVC.DAL.Entities;
using MVC.PL.Mapping;

namespace MVC.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            //Add Connection string
            builder.Services.AddDbContext<MvcAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //Service Of Connection String
            builder.Services.AddDbContext<MvcAppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectiion"));
            });
            //Service To Add Scoped
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository > ();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            //Service To Add Auto Mapper
            builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfiles()));
            //for token 
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Home/Error");
                });
            builder.Services.AddIdentity<ApplicationUser, RoleApplication>(options =>
            {
                options.Password.RequireDigit= true;
                options.Password.RequireLowercase= true;
                options.Password.RequireNonAlphanumeric= true;
                options.Password.RequireUppercase= true;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedAccount= false;
            }).AddEntityFrameworkStores<MvcAppDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}