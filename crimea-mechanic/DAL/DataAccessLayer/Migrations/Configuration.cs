using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Common.Constants;
using Common.Enums;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            CheckAndCreateRole(context, CommonRoles.Regular);
            CheckAndCreateRole(context, CommonRoles.CarService);
            CheckAndCreateRole(context, CommonRoles.Administrator);
            CheckAndCreateUser(context, "user@user.com", "password", CommonRoles.Regular);
            CheckAndCreateUserProfile(context, "user@user.com", "Test", "+7(978)-111-11-11");
            CheckAndCreateUser(context, "mechanic@mechanic.com", "password", CommonRoles.CarService);
            CheckAndCreateCarService(context, "mechanic@mechanic.com");
            CheckAndCreateUser(context, "admin@admin.com", "password", CommonRoles.Administrator);
        }

        #region User and Roles

        /// <summary>
        /// Создает роль по умолчанию
        /// </summary>
        private void CheckAndCreateRole(DatabaseContext context, string roleName)
        {
            if (context.Roles.Any(r => r.Name == roleName))
            {
                return;
            }
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var role = new IdentityRole
            {
                Name = roleName
            };
            manager.Create(role);
        }

        /// <summary>
        /// Создает пользователя по умолчанию
        /// </summary>
        private void CheckAndCreateUser(DatabaseContext context, string userName, string password, string roleName)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                var store = new AppUserStore(context);
                var manager = new UserManager<ApplicationUser>(store);
                user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    AccessFailedCount = 0
                };
                manager.Create(user, password);
                manager.AddToRole(user.Id, roleName);
            }
        }

        /// <summary>
        /// Создает профайл пользователя по умолчанию
        /// </summary>
        private void CheckAndCreateUserProfile(DatabaseContext context, string userName, string contactName, string phoneNumber)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null && user.UserProfile == null)
            {
                var profile = new UserProfile
                {
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Name = contactName,
                    Phone = phoneNumber
                };
                user.UserProfile = profile;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Создать автосервис по умолчанию
        /// </summary>
        private void CheckAndCreateCarService(DatabaseContext context, string userName)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null && user.CarService == null)
            {
                var carService = new CarService
                {
                    Name = "Test",
                    Address = "Test",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Email = "test@test.com",
                    ManagerName = "Test",
                    About = "Test test test",
                    TimetableWorks = "Test",
                    Site = "http://test.com",
                    State = CarServiceState.Active
                };
                user.CarService = carService;
                context.SaveChanges();
            }
        }

        #endregion User and Roles
    }
}
