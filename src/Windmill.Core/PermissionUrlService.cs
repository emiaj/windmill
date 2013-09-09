using FubuMVC.Core.Urls;

namespace Windmill.Core
{
    public interface IPermissionUrlService
    {
        string TemplateFor<TModel>(params string[] permissions) where TModel : class, new();
    }

    public class PermissionUrlService : IPermissionUrlService
    {
        private readonly IUrlRegistry _registry;
        private readonly IPermissionContext _context;

        public PermissionUrlService(IUrlRegistry registry, IPermissionContext context)
        {
            _registry = registry;
            _context = context;
        }

        public string TemplateFor<TModel>(params string[] permissions) where TModel : class, new()
        {
            return _context.HasAccessTo(permissions) ? _registry.TemplateFor<TModel>() : null;
        }
    }
}