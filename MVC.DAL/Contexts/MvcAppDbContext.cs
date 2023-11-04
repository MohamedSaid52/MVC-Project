using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL.Contexts
{
    public class MvcAppDbContext : IdentityDbContext<ApplicationUser,RoleApplication,string>
    {
        public MvcAppDbContext()
        {

        }
        public MvcAppDbContext(DbContextOptions<MvcAppDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  => optionsBuilder.UseSqlServer("server = DESKTOP-3OOPO38; database = MVCAppDb; integrated security = true ;TrustServerCertificate=True");

        public DbSet<Department> Departments {  get; set; } 
        public DbSet<Employee> Employees { get; set; }
    }
}
