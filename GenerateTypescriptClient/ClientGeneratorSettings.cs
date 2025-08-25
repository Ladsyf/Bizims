namespace GenerateTypescriptClient;

public class ClientGeneratorSettings
{
    public required Path Path { get; set; }
    public string SwaggerJsonUrl { get; set; } = string.Empty;
    public string ApiUrl { get; set; } = string.Empty;
}

public class Path
{ 
    public string Client { get; set; } = string.Empty;
}