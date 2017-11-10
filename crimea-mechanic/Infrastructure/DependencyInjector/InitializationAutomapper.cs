using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Managers.MapProfiles;

namespace DependencyInjector
{
    public class InitializationAutomapper
    {
        public static void Init()
        {
            var profiles = new List<Profile>
            {
                new RegistrationProfile(),
                new CarMarkProfile(),
                new CarModelProfile(),
                new WorkTagProfile(),
                new CityProfile()
            };
            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
