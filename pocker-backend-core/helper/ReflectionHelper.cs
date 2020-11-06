using System;
using System.Collections.Generic;
using System.Linq;

namespace pocker_backend_core.helper
{
    public static class ReflectionHelper
    {
        public static List<Type> GetSubclasses(Type type)
        {
            return type.Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(type) || IsGenericSubclassOf(t, type))
                .Where(t => !t.IsAbstract)
                .ToList();
        }

        private static bool IsGenericSubclassOf(Type t, Type t1)
        {
            while (t != null)
            {
                if (t.IsGenericType && t.GetGenericTypeDefinition() == t1) return true;
                t = t.BaseType;
            }

            return false;
        }
    }
}