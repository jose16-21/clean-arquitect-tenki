using System.Text.Json.Serialization;
using HolaMundoNet10.Presentation.Extensions;
using HolaMundoNet10.Presentation.Endpoints;
using HolaMundoNet10.Application.DTOs;
using HolaMundoNet10.Application.UseCases.Usuario;
using HolaMundoNet10.Application.UseCases.Formulario;
using HolaMundoNet10.Application.CQRS.Commands.Usuario;
using HolaMundoNet10.Application.CQRS.Commands.Formulario;
using HolaMundoNet10.Application.CQRS.Queries.Usuario;
using HolaMundoNet10.Application.CQRS.Queries.Formulario;

var builder = WebApplication.CreateBuilder(args);

// ==================== CONFIGURACIÓN ====================

// Configuración para Docker
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// Configuración de servicios con las mejoras de .NET 10
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

// ==================== REGISTRO DE SERVICIOS ====================

builder.Services.AddFluentValidators();
builder.Services.AddApplicationServices();
builder.Services.AddCQRSHandlers();
builder.Services.AddRepositories();
builder.Services.AddSwaggerDocumentation();

// ==================== CONSTRUCCIÓN DE LA APLICACIÓN ====================

var app = builder.Build();

// ==================== MIDDLEWARE ====================

// Habilitar Swagger en desarrollo y producción
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "HolaMundoNet10 API v1");
    options.RoutePrefix = "swagger";
    options.DocumentTitle = "HolaMundoNet10 API - Documentación";
});

// ==================== ENDPOINTS BÁSICOS ====================

app.MapGet("/", () => new
{
    mensaje = "¡Hola Mundo desde .NET 10 con Clean Architecture!",
    version = Environment.Version.ToString(),
    fecha = DateTime.Now,
    arquitectura = new
    {
        capas = new[] { "Presentation", "Application", "Domain", "Infrastructure" },
        patron = "Clean Architecture + CQRS",
        validacion = "Application Layer"
    },
    swagger = "/swagger"
})
.WithName("Root")
.WithTags("General")
.ExcludeFromDescription();

app.MapGet("/saludar/{nombre}", (string nombre) =>
    Results.Ok(new { saludo = $"¡Hola {nombre}!", framework = ".NET 10" }))
.WithName("Saludar")
.WithTags("General");

app.MapGet("/info", () => new InfoResponse(
    Framework: ".NET 10",
    Caracteristicas: new[]
    {
        "Clean Architecture Refactorizada",
        "CQRS Pattern",
        "FluentValidation",
        "Infrastructure Layer",
        "Repository Pattern",
        "Minimal APIs",
        "Dependency Injection",
        "JSON Source Generation",
        "C# 13"
    },
    Rendimiento: "Hasta 30% más rápido que .NET 8"
));

app.MapGet("/async", async () =>
{
    await Task.Delay(100);
    return Results.Ok(new
    {
        mensaje = "Operación async completada",
        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
    });
});

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    uptime = Environment.TickCount64,
    arquitectura = "Clean Architecture + CQRS"
}))
.WithName("HealthCheck")
.WithTags("Monitoring")
.Produces(200);

app.MapPost("/calcular", (CalculoRequest request) =>
{
    if (request.Numeros == null || request.Numeros.Length == 0)
        return Results.BadRequest("Debe proporcionar números");

    var suma = request.Numeros.Sum();
    var promedio = request.Numeros.Average();

    return Results.Ok(new CalculoResponse(
        Suma: suma,
        Promedio: promedio,
        Cantidad: request.Numeros.Length
    ));
})
.WithName("Calcular")
.WithTags("Legacy")
.Produces<CalculoResponse>(200)
.Produces(400);

// ==================== REGISTRO DE ENDPOINTS MODULARES ====================

app.MapUsuarioEndpoints();
app.MapFormularioEndpoints();

// ==================== INICIAR APLICACIÓN ====================

app.Run();

// ==================== RECORDS PARA ENDPOINTS SIMPLES ====================

record InfoResponse(string Framework, string[] Caracteristicas, string Rendimiento);
record CalculoRequest(int[] Numeros);
record CalculoResponse(int Suma, double Promedio, int Cantidad);

// ==================== JSON SOURCE GENERATION ====================

[JsonSerializable(typeof(InfoResponse))]
[JsonSerializable(typeof(CalculoRequest))]
[JsonSerializable(typeof(CalculoResponse))]
[JsonSerializable(typeof(UsuarioRequestDto))]
[JsonSerializable(typeof(UsuarioResponseDto))]
[JsonSerializable(typeof(FormularioRequestDto))]
[JsonSerializable(typeof(FormularioResponseDto))]
[JsonSerializable(typeof(ValidationResponseDto))]
[JsonSerializable(typeof(CrearUsuarioRequest))]
[JsonSerializable(typeof(CrearUsuarioResponse))]
[JsonSerializable(typeof(CrearFormularioRequest))]
[JsonSerializable(typeof(CrearFormularioResponse))]
[JsonSerializable(typeof(CrearUsuarioCommand))]
[JsonSerializable(typeof(CrearUsuarioCommandResponse))]
[JsonSerializable(typeof(CrearFormularioCommand))]
[JsonSerializable(typeof(CrearFormularioCommandResponse))]
[JsonSerializable(typeof(ObtenerUsuarioQuery))]
[JsonSerializable(typeof(ObtenerUsuarioQueryResponse))]
[JsonSerializable(typeof(ListarUsuariosQuery))]
[JsonSerializable(typeof(ListarUsuariosQueryResponse))]
[JsonSerializable(typeof(ObtenerFormularioQuery))]
[JsonSerializable(typeof(ObtenerFormularioQueryResponse))]
[JsonSerializable(typeof(ListarFormulariosQuery))]
[JsonSerializable(typeof(ListarFormulariosQueryResponse))]
[JsonSerializable(typeof(UsuarioDto))]
[JsonSerializable(typeof(FormularioDto))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
