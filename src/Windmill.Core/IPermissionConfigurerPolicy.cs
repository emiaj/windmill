namespace Windmill.Core
{
    public interface IPermissionConfigurerPolicy
    {
        void Apply(IPermissionRegistryCache cache);
    }
}