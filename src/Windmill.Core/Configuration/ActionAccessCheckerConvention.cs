using FubuMVC.Core;
using FubuMVC.Core.Registration;

namespace Windmill.Core.Configuration
{
    [Policy]
    public class ActionAccessCheckerConvention<THandler> : IConfigurationAction
    {
        private readonly string _actionName;
        private readonly string _permissionId;

        public ActionAccessCheckerConvention(string actionName,string permissionId)
        {
            _actionName = actionName;
            _permissionId = permissionId;
        }

        public void Configure(BehaviorGraph graph)
        {
            var action = graph.ActionsForHandler<THandler>().ByName(_actionName);
            var permissionToken = new PermissionToken(_permissionId);
            var policy = action.ParentChain().Authorization.AddPolicy(typeof(PermissionCheckerPolicy));
            action.Trace("Wrapping with security policy for permission:[" + _permissionId + "]");
            policy.DependencyByValue(permissionToken);
        }
    }
}