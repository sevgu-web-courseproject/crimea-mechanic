using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Managers;
using BusinessLogic.Managers.MapProfiles;
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

            var profiles = new List<Profile>
            {
                new RegistrationProfile()
            };
            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            Mapper.AssertConfigurationIsValid();

            return container;
        }
    }
}