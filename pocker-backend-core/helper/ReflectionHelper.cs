using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static void SetProperty(object obj, string propertyName, object val)
        {
            var type = obj.GetType();
            do
            {
                var property = type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (property!.CanWrite)
                {
                    property.SetValue(obj, val);
                    break;
                }

                type = type.BaseType;
            } while (type != null);
        }

        public static bool IsAssignableToGeneric(Type givenType, Type genericType)
        {
            if (givenType.GetInterfaces()
                .Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType)) return true;
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;
            if (givenType == genericType)
                return true;
            return givenType.BaseType != null && IsAssignableToGeneric(givenType.BaseType, genericType);
        }
    }
}