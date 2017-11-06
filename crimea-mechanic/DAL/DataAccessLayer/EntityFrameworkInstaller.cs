using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataAccessLayer.Models.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<DbContext>().ImplementedBy<DatabaseContext>());
            container.Register(Component.For<IUserStore<IApplicationUser>>().ImplementedBy<AppUserStore>().LifestyleTransient());
            container.Register(Component.For<IRoleStore<IdentityRole, string>>().ImplementedBy<AppRoleStore>().LifestyleTransient());
        }
    }
}
