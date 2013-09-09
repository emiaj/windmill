using FubuMVC.Core.Urls;

namespace Windmill.Core
{
    public interface IPermissionUrlService
    {
        string TemplateFor<TModel>(params string[] permissions) where TModel : class, new();
        string TemplateFor(object model, params string[] permissions);
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
            return _context.Check(permissions) ? _registry.TemplateFor<TModel>() : null;
        }

        public string TemplateFor(object model, params string[] permissions)
        {
            return _context.Check(permissions) ? _registry.TemplateFor(model) : null;
        }
    }
}