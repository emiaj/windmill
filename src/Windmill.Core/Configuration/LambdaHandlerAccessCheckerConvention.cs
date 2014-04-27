using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;

namespace Windmill.Core.Configuration
{
    public class LambdaHandlerAccessCheckerConvention : IConfigurationAction
    {
        private readonly Func<Type, bool> _predicate;
        private readonly string _permissionId;

        public LambdaHandlerAccessCheckerConvention(string permissionId, Func<Type, bool> predicate)
        {
            _predicate = predicate;
            _permissionId = permissionId;
        }

        public void Configure(BehaviorGraph graph)
        {
            graph.Actions().Where(x => _predicate(x.HandlerType))
                .Each(action =>
                {
                    var permissionToken = new PermissionToken(_permissionId);
                    var policy = action.ParentChain().Authorization.AddPolicy(typeof(PermissionCheckerPolicy));
                    //action.ParentChain().Trace("Wrapping with security policy for permission:[" + _permissionId + "]");
                    policy.DependencyByValue(permissionToken);
                });
        }
    }
}