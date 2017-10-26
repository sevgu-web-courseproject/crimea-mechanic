using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Registration.Lifestyle;

namespace Common.Castle.Windsor
{
    public static class LifestyleRegistrationExtensions
    {
        /// <summary>
        /// One component instance per web request, or if HttpContext is not available, scoped.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="group"></param>
        /// <returns></returns>
        public static ComponentRegistration<T> HybridPerWebRequestScoped<T>(this LifestyleGroup<T> group) where T : class
        {
            return group.Scoped<HybridPerWebRequestScopedScopeAccessor>();
        }
    }
}
