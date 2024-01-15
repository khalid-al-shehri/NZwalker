
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Web_API_Versioning;

public class ConfigurationSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) : IConfigureNamedOptions<SwaggerGenOptions>
{

    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider = apiVersionDescriptionProvider;

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(item.GroupName, CreateVersionInfo(item));
        }
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description){

        var info = new OpenApiInfo{
            Title = "Your Versioned Api",
            Version = description.ApiVersion.ToString()
        };

        return info;

    }
}