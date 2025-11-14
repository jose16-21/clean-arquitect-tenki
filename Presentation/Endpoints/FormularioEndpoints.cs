using HolaMundoNet10.Application.CQRS;
using HolaMundoNet10.Application.CQRS.Commands.Formulario;
using HolaMundoNet10.Application.CQRS.Queries.Formulario;
using HolaMundoNet10.Application.UseCases.Formulario;
using HolaMundoNet10.Application.DTOs;
using HolaMundoNet10.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolaMundoNet10.Presentation.Endpoints;

/// <summary>
/// Endpoints para gestión de formularios
/// </summary>
public static class FormularioEndpoints
{
    /// <summary>
    /// Registra todos los endpoints de formularios
    /// </summary>
    public static IEndpointRouteBuilder MapFormularioEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // Grupo CQRS (v3)
        var cqrsGroup = endpoints.MapGroup("/api/v3/formularios")
            .WithTags("Formularios (CQRS)");

        cqrsGroup.MapPost("/", CrearFormularioCQRS)
            .WithName("CrearFormularioCQRS")
            .Produces(201)
            .Produces(400);

        cqrsGroup.MapGet("/{id}", ObtenerFormularioCQRS)
            .WithName("ObtenerFormularioCQRS")
            .Produces(200)
            .Produces(404);

        cqrsGroup.MapGet("/", ListarFormulariosCQRS)
            .WithName("ListarFormulariosCQRS")
            .Produces(200);

        // Grupo Use Cases (v2)
        var useCasesGroup = endpoints.MapGroup("/api/v2/formularios")
            .WithTags("Formularios (Use Cases)");

        useCasesGroup.MapPost("/", CrearFormularioUseCase)
            .WithName("CrearFormularioV2")
            .Produces(201)
            .Produces(400);

        // Grupo Legacy
        var legacyGroup = endpoints.MapGroup("/api/formularios")
            .WithTags("Formularios (Legacy)");

        legacyGroup.MapPost("/", CrearFormularioLegacy)
            .WithName("CrearFormulario")
            .Produces<FormularioResponseDto>(201)
            .Produces(400);

        return endpoints;
    }

    private static async Task<IResult> CrearFormularioCQRS(
        CrearFormularioCommand command,
        ICommandHandler<CrearFormularioCommand, CrearFormularioCommandResponse> handler)
    {
        var response = await handler.HandleAsync(command);

        if (!response.Success)
        {
            return Results.BadRequest(new
            {
                mensaje = response.Mensaje,
                errores = response.Errores,
                advertencias = response.Advertencias,
                timestamp = DateTime.UtcNow
            });
        }

        return Results.Created($"/api/v3/formularios/{response.FormularioId}", new
        {
            id = response.FormularioId,
            titulo = response.Titulo,
            precioFinal = response.PrecioFinal,
            fechaCreacion = response.FechaCreacion,
            mensaje = response.Mensaje
        });
    }

    private static async Task<IResult> ObtenerFormularioCQRS(
        string id,
        IQueryHandler<ObtenerFormularioQuery, ObtenerFormularioQueryResponse> handler)
    {
        var query = new ObtenerFormularioQuery { FormularioId = id };
        var response = await handler.HandleAsync(query);

        if (!response.Success)
        {
            return Results.NotFound(new { mensaje = response.Mensaje });
        }

        return Results.Ok(response);
    }

    private static async Task<IResult> ListarFormulariosCQRS(
        int? Pagina,
        int? TamanoPagina,
        string? Categoria,
        bool? SoloActivos,
        IQueryHandler<ListarFormulariosQuery, ListarFormulariosQueryResponse> handler)
    {
        var query = new ListarFormulariosQuery
        {
            Pagina = Pagina ?? 1,
            TamanoPagina = TamanoPagina ?? 10,
            Categoria = Categoria,
            SoloActivos = SoloActivos
        };
        var response = await handler.HandleAsync(query);
        return Results.Ok(response);
    }

    private static async Task<IResult> CrearFormularioUseCase(
        CrearFormularioRequest request,
        ICrearFormularioUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(request);

        if (!response.Success)
        {
            return Results.BadRequest(new
            {
                mensaje = response.Mensaje,
                errores = response.Errores,
                advertencias = response.Advertencias,
                timestamp = DateTime.UtcNow
            });
        }

        return Results.Created($"/api/v2/formularios/{response.FormularioId}", new
        {
            id = response.FormularioId,
            titulo = response.Titulo,
            precioFinal = response.PrecioFinal,
            fechaCreacion = response.FechaCreacion,
            mensaje = response.Mensaje
        });
    }

    private static async Task<IResult> CrearFormularioLegacy(
        FormularioRequestDto request,
        IFormularioService formularioService)
    {
        var (success, response, validation) = await formularioService.CrearFormularioAsync(request);

        if (!success)
        {
            return Results.BadRequest(new
            {
                mensaje = "Errores de validación",
                validacion = validation,
                timestamp = DateTime.UtcNow
            });
        }

        return Results.Created($"/api/formularios/{response!.Id}", response);
    }
}
