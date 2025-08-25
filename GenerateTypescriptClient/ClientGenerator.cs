using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.OperationNameGenerators;
using NSwag.CodeGeneration.TypeScript;

namespace GenerateTypescriptClient
{
    public static class ClientGenerator
    {
        public static async Task GenerateWebClientAsync(ClientGeneratorSettings settings)
        {
            var document = await OpenApiDocument.FromUrlAsync(settings.SwaggerJsonUrl);

            await GenerateClient(settings.Path.Client, settings.ApiUrl,() =>
            {
                var settings = new TypeScriptClientGeneratorSettings();

                settings.TypeScriptGeneratorSettings.TypeStyle = TypeScriptTypeStyle.Interface;
                settings.TypeScriptGeneratorSettings.TypeScriptVersion = 5.5M;
                settings.Template = TypeScriptTemplate.Angular;
                settings.TypeScriptGeneratorSettings.DateTimeType = TypeScriptDateTimeType.String;
                settings.HttpClass = HttpClass.HttpClient;
                settings.WithCredentials = false;
                settings.UseSingletonProvider = true;
                settings.InjectionTokenType = InjectionTokenType.InjectionToken;
                settings.RxJsVersion = 7.8M;
                settings.BaseUrlTokenName = "API_BASE_URL";

                settings.UseSingletonProvider = false;

                settings.OperationNameGenerator = new MultipleClientsFromFirstTagAndOperationIdGenerator();

                settings.GenerateOptionalParameters = true;

                settings.IncludeHttpContext = true;

                var generator = new TypeScriptClientGenerator(document, settings);
                var code = generator.GenerateFile();

                return (document, code);
            });
        }

        async static Task GenerateClient(string generatePath, string apiUrl, Func<(OpenApiDocument, string)> generateCode)
        {
            Console.WriteLine($"Generating ${generatePath}");
            var (document, code) = generateCode();

            code = code.ReplaceInGeneratedText(new Dictionary<string, string>() { { $"\"{apiUrl}\"", "\"\"" } });

            await File.WriteAllTextAsync(generatePath, code);
            Console.WriteLine($"{generatePath} successfully completed");
        }

        static string ReplaceInGeneratedText(this string source, Dictionary<string, string> replace)
        {
            var newSource = string.Empty;
            foreach (var (key, value) in replace)
            {
                newSource = source.Replace(key, value);
            }

            return newSource;
        }
    }
}