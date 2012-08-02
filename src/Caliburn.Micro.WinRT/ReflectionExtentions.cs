using System;
using System.Reflection;

namespace Caliburn.Micro
{
    public static class ReflectionExtentions
    {
        public static bool IsAssignableFrom(this Type target, Type type)
        {
            return target.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }
    }
}
