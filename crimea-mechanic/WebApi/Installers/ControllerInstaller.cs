using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebApi.Controllers;

namespace WebApi.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        private readonly Assembly _assembly;
        public ControllerInstaller(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssembly(_assembly).BasedOn<ApiControllerBase>().LifestyleTransient());
        }
    }
}