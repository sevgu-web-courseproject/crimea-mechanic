using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
{
    public class AppUserStore : UserStore<ApplicationUser>, IUserStore<IApplicationUser>, IUserRoleStore<IApplicationUser>, IUserPasswordStore<IApplicationUser>
    {
        public AppUserStore(DbContext context) : base(context)
        {
        }

        public Task CreateAsync(IApplicationUser user)
        {
            return base.CreateAsync(user as ApplicationUser);
        }

        public async Task UpdateAsync(IApplicationUser user)
        {
            var u = await base.FindByIdAsync(user.Id);
            await base.UpdateAsync(u);
        }

        public Task DeleteAsync(IApplicationUser user)
        {
            return base.DeleteAsync(user as ApplicationUser);
        }

        public new async Task<IApplicationUser> FindByIdAsync(string userId)
        {
            return await base.FindByIdAsync(userId);
        }

        public new async Task<IApplicationUser> FindByNameAsync(string userName)
        {
            return await base.FindByNameAsync(userName);
        }

        public Task SetPasswordHashAsync(IApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(IApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IApplicationUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task AddToRoleAsync(IApplicationUser user, string roleName)
        {
            return base.AddToRoleAsync(user as ApplicationUser, roleName);
        }

        public Task RemoveFromRoleAsync(IApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(IApplicationUser user)
        {
            return base.GetRolesAsync(user as ApplicationUser);
        }

        public Task<bool> IsInRoleAsync(IApplicationUser user, string roleName)
        {
            return base.IsInRoleAsync(user as ApplicationUser, roleName);
        }
    }
}
