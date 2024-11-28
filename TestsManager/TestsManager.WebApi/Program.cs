using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using TestsManager.Application;
using TestsManager.Application.InputModels.Answer;
using TestsManager.Application.ViewModels.Answer;
using TestsManager.Application.ViewModels.Questions;
using TestsManager.DataAccess.Context;
using TestsManager.WebApi.ExceptionHandlers;
using Topshelf;

namespace TestsManager.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        HostFactory.Run(x =>
        {
            x.Service<Service>(s =>
            {
                s.ConstructUsing(_ => new Service());
                s.WhenStarted(f => f.Start(args));
                s.WhenStopped(f => f.Stop());
            });

            x.SetDescription(Constants.ServerServiceDescription);
            x.SetDisplayName(Constants.ServerServiceName);
            x.SetServiceName(Constants.ServerServiceName);
        });
    }
}

internal class Service
{
    private Thread _thread;

    public Service()
    {
        _thread = new Thread(Start);
    }

    public void Start(string[] args) => _thread.Start(args);

    public void Stop()
    {
        _thread.Interrupt();
    }

    private void Start(object? aruments)
    {
        var args = aruments as string[];

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
                o.SerializerSettings.Converters.Add(
                    new JsonTestsManagerInheritanceConverter(typeof(AnswerInputModelBase),
                        Constants.DiscriminatorName));
                o.SerializerSettings.Converters.Add(
                    new JsonTestsManagerInheritanceConverter(typeof(AnswerViewModelBase), Constants.DiscriminatorName));
                o.SerializerSettings.Converters.Add(
                    new JsonTestsManagerInheritanceConverter(typeof(QuestionViewModelBase),
                        Constants.DiscriminatorName));
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestsManager.WebApi", Version = "v1" });

            c.UseOneOfForPolymorphism();
            c.UseAllOfForInheritance();

            c.SchemaFilter<ServicesExtensions.NSwagDiscriminatorSchemaFilter>();
        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: Constants.CorsPolicyName, builder =>
            {
                builder.WithOrigins(Constants.AllowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>();
        dbContextOptions.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
        builder.Services.AddTransient<DatabaseContext>(_ => new DatabaseContext(dbContextOptions.Options));
        
        builder.Services.RegisterAbstractions();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(Constants.CorsPolicyName);
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
