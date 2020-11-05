using System;
using System.Collections.Generic;
using System.Linq;
using pocker_backend_core.messages.interaction;

namespace pocker_backend_core.helpers
{
    public static class Support
    {
        public static void GenerateSchemasForInteractions()
        {
            var requestTypes = ReflectionHelper.GetSubclasses(typeof(AbstractRequest<>));
            var responseTypes = ReflectionHelper.GetSubclasses(typeof(AbstractResponse));
            PrintTypeSchemas("REQUESTS SCHEMAS:", requestTypes);
            PrintTypeSchemas("RESPONSES SCHEMAS:", responseTypes);
        }

        private static void PrintTypeSchemas(string desc, IEnumerable<Type> types)
        {
            Console.WriteLine(desc);
            types.Select(JsonHelper.SchemaFor).ToList().ForEach(Console.WriteLine);
            Console.WriteLine(Enumerable.Repeat("=", 79).Aggregate((x, y) => x + y));
        }
    }
}