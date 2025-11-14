using FluentValidation;
using HolaMundoNet10.Application.Services;
using HolaMundoNet10.Application.UseCases.Usuario;
using HolaMundoNet10.Application.UseCases.Formulario;
using HolaMundoNet10.Application.CQRS;
using HolaMundoNet10.Application.CQRS.Commands.Usuario;
using HolaMundoNet10.Application.CQRS.Commands.Formulario;
using HolaMundoNet10.Application.CQRS.Queries.Usuario;
using HolaMundoNet10.Application.CQRS.Queries.Formulario;
using HolaMundoNet10.Application.Validators;
using HolaMundoNet10.Infrastructure.Persistence.Repositories;

namespace HolaMundoNet10.Presentation.Extensions;

/// <summary>
/// Extensiones para configurar servicios en el contenedor de DI
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra todos los servicios de aplicación
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Servicios legacy
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IFormularioService, FormularioService>();

        // Use Cases (Clean Architecture)
        services.AddScoped<ICrearUsuarioUseCase, CrearUsuarioUseCase>();
        services.AddScoped<ICrearFormularioUseCase, CrearFormularioUseCase>();

        return services;
    }

    /// <summary>
    /// Registra todos los handlers de CQRS
    /// </summary>
    public static IServiceCollection AddCQRSHandlers(this IServiceCollection services)
    {
        // Command Handlers
        services.AddScoped<ICommandHandler<CrearUsuarioCommand, CrearUsuarioCommandResponse>, 
            CrearUsuarioCommandHandler>();
        services.AddScoped<ICommandHandler<CrearFormularioCommand, CrearFormularioCommandResponse>, 
            CrearFormularioCommandHandler>();

        // Query Handlers
        services.AddScoped<IQueryHandler<ObtenerUsuarioQuery, ObtenerUsuarioQueryResponse>, 
            ObtenerUsuarioQueryHandler>();
        services.AddScoped<IQueryHandler<ListarUsuariosQuery, ListarUsuariosQueryResponse>, 
            ListarUsuariosQueryHandler>();
        services.AddScoped<IQueryHandler<ObtenerFormularioQuery, ObtenerFormularioQueryResponse>, 
            ObtenerFormularioQueryHandler>();
        services.AddScoped<IQueryHandler<ListarFormulariosQuery, ListarFormulariosQueryResponse>, 
            ListarFormulariosQueryHandler>();

        return services;
    }

    /// <summary>
    /// Registra todos los validadores de FluentValidation
    /// </summary>
    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();
        return services;
    }

    /// <summary>
    /// Registra todos los repositorios de Infrastructure
    /// </summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Repositorios en memoria (para demo)
        // En producción, usar implementaciones con EF Core o Dapper
        services.AddSingleton<IUsuarioRepository, InMemoryUsuarioRepository>();
        services.AddSingleton<IFormularioRepository, InMemoryFormularioRepository>();

        return services;
    }

    /// <summary>
    /// Configura Swagger/OpenAPI
    /// </summary>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "HolaMundoNet10 API",
                Version = "v1",
                Description = "API con Clean Architecture, CQRS, FluentValidation y .NET 10",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Equipo de Desarrollo",
                    Email = "dev@example.com"
                }
            });
        });

        return services;
    }
}
