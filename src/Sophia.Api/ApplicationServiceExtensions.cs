namespace Sophia.Api;

using System.Text.Json.Serialization;
using Sophia.Api.Converters;
using Sophia.Infrastructure;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
    }

    public static WebApplicationBuilder AddApplicationBuilder(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new JstDateTimeConverter());
            });

        builder.Services.AddApplicationServices();

        builder.Services.AddSwaggerGen();

        builder.Services.AddRepository();

        return builder;
    }
}
