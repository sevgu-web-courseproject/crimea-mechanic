using System.Data.Entity;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor

        public DatabaseContext() : base("DatabaseConnectionString") { }

        #endregion

        #region DbSets

        public DbSet<CarService> CarServices { get; set; }
        public DbSet<CarServicePhone> CarServicePhones { get; set; }
        public DbSet<CarServiceFile> CarServiceFiles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WorkTag> WorkTags { get; set; }
        public DbSet<CarMark> CarMarks { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserCar> UserCars { get; set; }

        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<CarService>()
                .HasRequired(c => c.ApplicationUser)
                .WithOptional(c => c.CarService);

            modelBuilder.Entity<UserProfile>()
                .HasRequired(c => c.ApplicationUser)
                .WithOptional(c => c.UserProfile);
        }

        #endregion
    }
}
