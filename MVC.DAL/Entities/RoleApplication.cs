using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL.Entities
{
    public class RoleApplication:IdentityRole
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
