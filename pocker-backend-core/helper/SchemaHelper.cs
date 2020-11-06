using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using pocker_backend_core.messaging;

namespace pocker_backend_core.helper
{
    public static class SchemaHelper
    {
        private static readonly DirectoryInfo SchemasDir =
            new DirectoryInfo(ConfigurationManager.AppSettings["schemasDir"]);

        public static void GenerateSchemasForInteractions()
        {
            if (SchemasDir.Exists) SchemasDir.Delete(true);
            SchemasDir.Create();
            new DirectoryInfo(Path.Combine(SchemasDir.FullName, "out")).Create();
            var requestTypes = ReflectionHelper.GetSubclasses(typeof(AbstractRequest<>));
            var responseTypes = ReflectionHelper.GetSubclasses(typeof(AbstractResponse));
            GenerateSchemasForTypes("GENERATE REQUESTS SCHEMAS:", requestTypes);
            GenerateSchemasForTypes("GENERATE RESPONSES SCHEMAS:", responseTypes);
            ZipFile.CreateFromDirectory(Path.Combine(SchemasDir.FullName, "out"),
                Path.Combine(SchemasDir.FullName, "schemas.zip"));
        }

        private static void GenerateSchemasForTypes(string desc, IEnumerable<Type> types)
        {
            Console.WriteLine(desc);
            types.ToList().ForEach(x =>
            {
                var schemaJson = JsonHelper.SchemaFor(x).ToString();
                var exampleJson = JsonHelper.Serialize(JsonHelper.Deserialize("{}", x));
                var exampleJsonPath = Path.Combine(SchemasDir.FullName, "out", $"{x.FullName}.example.json");
                var schemaJsonPath = Path.Combine(SchemasDir.FullName, "out", $"{x.FullName}.schema.json");
                File.WriteAllText(exampleJsonPath, exampleJson);
                File.WriteAllText(schemaJsonPath, schemaJson);
            });
            Console.WriteLine(Enumerable.Repeat("=", 79).Aggregate((x, y) => x + y));
        }
    }
}