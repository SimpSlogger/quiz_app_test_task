using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TestsManager.Application.Services.AnswersCheck;
using TestsManager.Application.Services.TableFile;
using TestsManager.Application.Services.Tests;
using TestsManager.Core.Adapters.Repositories;
using TestsManager.DataAccess.Repositories;

namespace TestsManager.WebApi;

public static class ServicesExtensions
{
    public static void RegisterAbstractions(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<ITestRepository, TestRepository>();

        // Services
        services.AddScoped<ITableFileService, TableFileService>();
        services.AddScoped<IAnswersCheckService, AnswersCheckService>();
        services.AddScoped<ITestsService, TestsService>();
    }

    /// <summary>
    /// Добавить свойство "discriminator" ко входным моделям и моделям представления,
    /// необходимое для корректного определения подтипов в сгенерированном с помощью NSwagStudio клиенте
    /// </summary>
    public class NSwagDiscriminatorSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.Assembly.GetTypes().Any(t => t.IsSubclassOf(context.Type)))
            {
                return;
            }

            schema.Properties.Add(Constants.DiscriminatorName, new OpenApiSchema()
            {
                Type = "string",
                Nullable = true
            });

            schema.Required.Add(Constants.DiscriminatorName);
            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = Constants.DiscriminatorName
            };
        }
    }
}
