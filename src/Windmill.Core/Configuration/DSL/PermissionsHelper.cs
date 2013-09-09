using System;
using System.Linq.Expressions;
using FubuCore.Reflection;
using FubuMVC.Core;

namespace Windmill.Core.Configuration.DSL
{
    public static class PermissionsHelper
    {
        public static FubuRegistry AttachPermissionToHandler<THandler>(this FubuRegistry registry, string permissionId)
        {
            registry.Policies.Add(new HandlerAccessCheckerConvention<THandler>(permissionId));
            return registry;
        }

        public static FubuRegistry AttachPermissionToHandler(this FubuRegistry registry, string permissionId, Func<Type, bool> predicate)
        {
            registry.Policies.Add(new LambdaHandlerAccessCheckerConvention(permissionId, predicate));
            return registry;
        }
        public static FubuRegistry AttachPermissionToHandlerUnderNamespace(this FubuRegistry registry, string permissionId, string ns)
        {
            registry.Policies.Add(new LambdaHandlerAccessCheckerConvention(permissionId, x => x.Namespace.StartsWith(ns)));
            return registry;
        }

        public static FubuRegistry AttachPermissionToAction<THandler>(this FubuRegistry registry, Expression<Func<THandler, object>> expression, string permissionId)
        {
            var methodName = ReflectionHelper.GetMethod(expression).Name;
            registry.Policies.Add(new ActionAccessCheckerConvention<THandler>(methodName, permissionId));
            return registry;
        }

        public static FubuRegistry AttachPermissionToAction<THandler>(this FubuRegistry registry, Expression<Action<THandler>> expression, string permissionId)
        {
            var methodName = ReflectionHelper.GetMethod(expression).Name;
            registry.Policies.Add(new ActionAccessCheckerConvention<THandler>(methodName, permissionId));
            return registry;
        }

    }
}