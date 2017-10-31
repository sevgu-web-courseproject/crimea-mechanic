using BusinessLogic.Managers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataAccessLayer;
using DataAccessLayer.Repositories;

namespace DependencyInjector
{
    public class DependencyInjector
    {
        public static WindsorContainer InitContainer(IWindsorInstaller installer = null)
        {
            var container = new WindsorContainer();

            container.Install(new BusinessLogicInstaller());
            container.Install(new EntityFrameworkInstaller());
            container.Install(new RepositoriesInstaller());

            if (installer != null)
            {
                container.Install(installer);
            }

            return container;
        }
    }
}
