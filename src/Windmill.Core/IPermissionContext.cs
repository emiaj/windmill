using System.Collections.Generic;
using FubuMVC.Core.Security;

namespace Windmill.Core
{
    public interface IPermissionContext
    {
        bool HasAccessTo(string permission);
        bool HasAccessTo(IEnumerable<string> permissions);
        AuthorizationRight RightsTo(string permission);
    }
}