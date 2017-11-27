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
            container.Register(Classes.FromThisAssembly().BasedOn<BaseManager>()
                .WithService
                .DefaultInterfaces()
                .LifestyleTransient());
            container.Register(Component.For<IUserManager, IUserInternalManager>().ImplementedBy<UserManager>().LifestyleTransient());
            container.Register(Component.For<IValidationManager>().ImplementedBy<ValidationManager>().LifestyleTransient());
            container.Register(Component.For<IFileManager>().ImplementedBy<FileManager>().LifestylePerWebRequest());
        }
    }
}
