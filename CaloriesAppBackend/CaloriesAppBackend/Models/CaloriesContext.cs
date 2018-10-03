using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CaloriesAppBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductUser> ProductUsers { get; set; }
        public UserInfo UserInfo { get; set; }

        public ApplicationUser()
        {
            Products = new List<Product>();
            ProductUsers = new List<ProductUser>();
        }

    }


    public class CaloriesContext : IdentityDbContext<ApplicationUser>
    {
        public CaloriesContext(DbContextOptions<CaloriesContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductUser> ProductUsers { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Interpretation> Interpretations { get; set; }
    }
}
