using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
{
    public class AppRoleStore : RoleStore<IdentityRole>
    {
        public AppRoleStore(DbContext context) : base(context)
        {
        }
    }
}
