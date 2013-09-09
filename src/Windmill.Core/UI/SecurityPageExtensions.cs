using FubuMVC.Core;
using FubuMVC.Core.View;

namespace Windmill.Core.UI
{
    public static class SecurityPageExtensions
    {
        public static bool IsAuthorized(this IFubuPage page, object model)
        {
             return page.Get<IEndpointService>().EndpointFor(model).IsAuthorized;
         }
    }
}