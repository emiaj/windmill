using System.Collections.Generic;
using Bottles;
using Bottles.Diagnostics;

namespace Windmill.Core
{
    public class PermissionPolicyRunner : IActivator
    {
        private readonly IEnumerable<IPermissionConfigurerPolicy> _policies;
        private readonly IPermissionRegistryCache _cache;

        public PermissionPolicyRunner(IEnumerable<IPermissionConfigurerPolicy> policies, IPermissionRegistryCache cache)
        {
            _policies = policies;
            _cache = cache;
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            _policies.Each(x => x.Apply(_cache));
        }
    }
}