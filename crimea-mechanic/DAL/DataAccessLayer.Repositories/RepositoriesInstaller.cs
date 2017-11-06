using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataAccessLayer.Repositories.Abstraction;

namespace DataAccessLayer.Repositories
{
    public class RepositoriesInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IRepositoryBase>()
                .WithService
                .FromInterface()
                .LifestyleTransient());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>());
            container.Register(Component.For<IRepositoryFactory>().ImplementedBy<RepositoryFactory>().LifestyleTransient());
        }
    }
}
