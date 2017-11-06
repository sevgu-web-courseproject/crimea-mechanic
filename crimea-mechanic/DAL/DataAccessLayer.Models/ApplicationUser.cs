using DataAccessLayer.Models.Abstraction;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser, IEntity<string>
    {
        /// <summary>
        /// Профайл пользователя
        /// </summary>
        public virtual UserProfile UserProfile { get; set; }

        /// <summary>
        /// Автосервис
        /// </summary>
        public virtual CarService CarService { get; set; }
    }
}
