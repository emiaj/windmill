using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Registration;

namespace Windmill.Core.Configuration
{
    public class HandlerAccessCheckerConvention<THandler> : IConfigurationAction
    {
        private readonly string _permissionId;
        public HandlerAccessCheckerConvention(string permissionId)
        {
            _permissionId = permissionId;
        }

        public void Configure(BehaviorGraph graph)
        {
            graph.ActionsForHandler<THandler>()
                .Each(action =>
                {
                    var permissionToken = new PermissionToken(_permissionId);
                    var policy = action.ParentChain().Authorization.AddPolicy(typeof(PermissionCheckerPolicy));
                    //action.Trace("Wrapping with security policy for permission:[" + _permissionId + "]");
                    policy.DependencyByValue(permissionToken);
                });
        }
    }
}