using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;

namespace Windmill.Core.Configuration
{
    public class PermissionCheckerPolicy : IAuthorizationPolicy
    {
        private readonly PermissionToken _permissionToken;
        private readonly IPermissionContext _permissionContext;
        public PermissionCheckerPolicy(PermissionToken permissionToken, IPermissionContext permissionContext)
        {
            _permissionToken = permissionToken;
            _permissionContext = permissionContext;
        }

        public AuthorizationRight RightsFor(IFubuRequest request)
        {
            var authorizationRight = _permissionContext.RightsTo(_permissionToken.Id);
            return authorizationRight;
        }
    }

}