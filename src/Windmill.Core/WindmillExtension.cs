using Bottles;
using FubuMVC.Core;

namespace Windmill.Core
{
    public class WindmillExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Services(x => x.SetServiceIfNone<IPermissionContext, PermissionContext>());
            registry.Services(x => x.SetServiceIfNone<IPermissionProviderCache, PermissionProviderCache>());
            registry.Services(x => x.SetServiceIfNone<IPermissionRegistryCache>(new PermissionRegistryCache()));
            registry.Services(x => x.FillType<IActivator, PermissionPolicyRunner>());
            registry.Services(x => x.SetServiceIfNone<IPermissionUrlService, PermissionUrlService>());
            registry.Services(x => x.SetServiceIfNone<IPermissionProvider>(new InMemoryPermissionProvider()));
            registry.Services(x => x.SetServiceIfNone<IRoleByUserProvider>(new NulloRoleByUserProvider()));
        }
    }
}