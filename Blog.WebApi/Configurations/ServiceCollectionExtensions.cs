using Blog.Application.Interfaces;
using Blog.Application.Mappings;
using Blog.Application.Services;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Blog.WebApi.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));

        // Add AutoMapper
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Register Services
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IClaimProvider, ClaimProvider>();

        // Register Infrastructure Services
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        // Register all repositories
        services.Scan(scan => scan
            .FromAssemblyOf<BlogDbContext>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Register all application services
        services.Scan(scan => scan
            .FromAssemblyOf<IPostService>()
            .AddClasses(classes => classes.Where(type => 
                type.Name.EndsWith("Service") && 
                !type.Name.StartsWith("DateTime") && 
                !type.Name.StartsWith("CurrentUser")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Add MediatR
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(typeof(IPostService).Assembly));

        // Add Fluent Validation
        services.AddValidatorsFromAssembly(typeof(IPostService).Assembly);

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Caching
        services.AddMemoryCache();
        services.AddDistributedMemoryCache();

        // Add Response Compression
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
        });

        // Add Health Checks
        services.AddHealthChecks()
            .AddDbContextCheck<BlogDbContext>()
            .AddCheck<DatabaseHealthCheck>("Database");

        return services;
    }

    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {

        // Add CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            options.AddPolicy("AllowSpecific", builder =>
            {
                var allowedOrigins = configuration.GetSection("Cors:Origins").Get<string[]>();
                builder.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            });
        });

        return services;
    }
}

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly BlogDbContext _context;

    public DatabaseHealthCheck(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Try to get a simple query result
            await _context.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(exception: ex);
        }
    }
}
