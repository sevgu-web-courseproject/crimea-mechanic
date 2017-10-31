using DataAccessLayer.Models.Abstraction;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser, IEntity<string>
    {
    }
}
