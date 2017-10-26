using BusinessLogic.Managers.Abstraction;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BusinessLogic.Managers
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserManager>().ImplementedBy<UserManager>().LifestyleTransient());
        }
    }
}
