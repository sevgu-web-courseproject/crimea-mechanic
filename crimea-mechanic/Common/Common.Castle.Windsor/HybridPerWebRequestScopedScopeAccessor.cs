using Castle.MicroKernel.Lifestyle;

namespace Common.Castle.Windsor
{
    public class HybridPerWebRequestScopedScopeAccessor : HybridPerWebRequestScopeAccessor
    {
        public HybridPerWebRequestScopedScopeAccessor() : base(new LifetimeScopeAccessor())
        {
        }
    }
}
