using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace pocker_backend_core.helper
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        private static readonly JSchemaGenerator Generator = new JSchemaGenerator();

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }

        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, Settings);
        }

        public static JSchema SchemaFor(Type type)
        {
            var schema = Generator.Generate(type);
            var type1 = Generator.Generate(typeof(string));
            type1.Const = type.FullName;
            type1.Description = "Type of object represented by this schema";
            CollectionHelper.AddToStart(schema.Properties, "$type", type1);
            schema.Required.Insert(0, "$type");
            return schema;
        }
    }
}