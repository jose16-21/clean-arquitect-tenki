using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using HolaMundoNet10.Application.Services;
using HolaMundoNet10.Application.DTOs;
using HolaMundoNet10.Domain.Validators;

var builder = WebApplication.CreateBuilder(args);

// Configuración para Docker
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// Registrar FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();

// Registrar servicios de la aplicación - Inyección de Dependencias
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IFormularioService, FormularioService>();

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "HolaMundoNet10 API",
        Version = "v1",
        Description = "API con Clean Architecture, FluentValidation y .NET 10",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipo de Desarrollo",
            Email = "dev@example.com"
        }
    });
});

// Configuración de servicios con las mejoras de .NET 10
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

// Habilitar Swagger en desarrollo y producción
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "HolaMundoNet10 API v1");
    options.RoutePrefix = "swagger";
    options.DocumentTitle = "HolaMundoNet10 API - Documentación";
});

// ==================== ENDPOINTS BÁSICOS ====================

app.MapGet("/", () => new { 
    mensaje = "¡Hola Mundo desde .NET 10 con Clean Architecture!", 
    version = Environment.Version.ToString(),
    fecha = DateTime.Now,
    arquitectura = new {
        capas = new[] { "Presentation", "Application", "Domain", "Infrastructure" },
        patron = "Clean Architecture",
        validacion = "Capa de Domain"
    },
    swagger = "/swagger"
})
.WithName("Root")
.WithTags("General")

.ExcludeFromDescription();

// Endpoint con parámetros de ruta
app.MapGet("/saludar/{nombre}", (string nombre) => 
    Results.Ok(new { saludo = $"¡Hola {nombre}!", framework = ".NET 10" }))
.WithName("Saludar")
.WithTags("General")
;

// Endpoint con Native AOT compatible
app.MapGet("/info", () => new InfoResponse(
    Framework: ".NET 10",
    Caracteristicas: new[]
    {
        "Clean Architecture",
        "FluentValidation",
        "Minimal APIs mejoradas",
        "Validación declarativa",
        "Inyección de dependencias",
        "JSON Source Generation",
        "Mejor rendimiento JIT",
        "C# 13"
    },
    Rendimiento: "Hasta 30% más rápido que .NET 8"
));

// Endpoint async con operaciones I/O
app.MapGet("/async", async () =>
{
    await Task.Delay(100); // Simula operación async
    return Results.Ok(new { 
        mensaje = "Operación async completada",
        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
    });
});

// Health check
app.MapGet("/health", () => Results.Ok(new { 
    status = "healthy", 
    uptime = Environment.TickCount64,
    arquitectura = "Clean Architecture"
}))
.WithName("HealthCheck")
.WithTags("Monitoring")

.Produces(200);

// ==================== ENDPOINTS CON CLEAN ARCHITECTURE ====================

// Endpoint para crear usuario con validaciones en Domain
app.MapPost("/api/usuarios", async (UsuarioRequestDto request, IUsuarioService usuarioService) =>
{
    var (success, response, validation) = await usuarioService.CrearUsuarioAsync(request);

    if (!success)
    {
        return Results.BadRequest(new {
            mensaje = "Errores de validación",
            validacion = validation,
            timestamp = DateTime.UtcNow
        });
    }

    return Results.Created($"/api/usuarios/{response!.Id}", response);
})
.WithName("CrearUsuario")
.WithTags("Usuarios")

.Produces<UsuarioResponseDto>(201)
.Produces(400);

// Endpoint para crear formulario con validaciones en Domain
app.MapPost("/api/formularios", async (FormularioRequestDto request, IFormularioService formularioService) =>
{
    var (success, response, validation) = await formularioService.CrearFormularioAsync(request);

    if (!success)
    {
        return Results.BadRequest(new {
            mensaje = "Errores de validación",
            validacion = validation,
            timestamp = DateTime.UtcNow
        });
    }

    return Results.Created($"/api/formularios/{response!.Id}", response);
})
.WithName("CrearFormulario")
.WithTags("Formularios")

.Produces<FormularioResponseDto>(201)
.Produces(400);

// ==================== ENDPOINT LEGACY (para comparación) ====================

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
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
