using System.Collections.Generic;
using FubuMVC.Core.Security;

namespace Windmill.Core
{
    public interface IPermissionContext
    {
        bool Check(string permission);
        bool Check(IEnumerable<string> permissions);
        AuthorizationRight RightsTo(string permission);
    }
}