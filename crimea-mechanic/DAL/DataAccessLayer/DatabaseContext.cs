using System.Data.Entity;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext() : base("DatabaseConnectionString")
        {

        }

        public DbSet<CarService> CarServices { get; set; }
        public DbSet<CarServicePhone> CarServicePhones { get; set; }
        public DbSet<CarServiceFile> CarServiceFiles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<CarModelTag> CarModelTags { get; set; }
        public DbSet<WorkTag> WorkTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}
