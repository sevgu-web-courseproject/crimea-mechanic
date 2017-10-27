using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Common.Constants;
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
            CheckAndCreateRole(context, CommonRoles.Mechanic);
            CheckAndCreateRole(context, CommonRoles.Administrator);
            CheckAndCreateUser(context, "user@user.com", "password", CommonRoles.Regular);
            CheckAndCreateUser(context, "mechanic@mechanic.com", "password", CommonRoles.Mechanic);
            CheckAndCreateUser(context, "admin@admin.com", "password", CommonRoles.Administrator);
        }

        #region User and Roles

        /// <summary>
        /// Создает роль по умолчанию
        /// </summary>
        /// <param name="context">контекст</param>
        /// <param name="roleName">роль</param>
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
        /// <param name="context">контекст</param>
        /// <param name="userName">имя полоьзователя</param>
        /// <param name="password">пароль</param>
        /// <param name="roleName">роль</param>
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

        #endregion User and Roles
    }
}
