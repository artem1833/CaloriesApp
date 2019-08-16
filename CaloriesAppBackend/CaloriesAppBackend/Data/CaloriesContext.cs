using System.Collections.Generic;
using CaloriesAppBackend.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CaloriesAppBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<ProductUser> ProductUsers { get; set; }
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
        //public DbSet<Interpretation> Interpretations { get; set; }
        public DbSet<GenderInterpretation> GenderInterpretations { get; set; }
        public DbSet<PhysicalActivityInterpretation> PhysicalActivityInterpretations { get; set; }
        public DbSet<PurposeInterpretation> PurposeInterpretations { get; set; }
        public DbSet<UnitOfMeasureInterpretation> UnitOfMeasureInterpretations { get; set; }

    }
}
