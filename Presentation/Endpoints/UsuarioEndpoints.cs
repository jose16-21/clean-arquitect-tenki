using HolaMundoNet10.Application.CQRS;
using HolaMundoNet10.Application.CQRS.Commands.Usuario;
using HolaMundoNet10.Application.CQRS.Queries.Usuario;
using HolaMundoNet10.Application.UseCases.Usuario;
using HolaMundoNet10.Application.DTOs;
using HolaMundoNet10.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolaMundoNet10.Presentation.Endpoints;

/// <summary>
/// Endpoints para gestión de usuarios
/// </summary>
public static class UsuarioEndpoints
{
    /// <summary>
    /// Registra todos los endpoints de usuarios
    /// </summary>
    public static IEndpointRouteBuilder MapUsuarioEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // Grupo CQRS (v3)
        var cqrsGroup = endpoints.MapGroup("/api/v3/usuarios")
            .WithTags("Usuarios (CQRS)");

        cqrsGroup.MapPost("/", CrearUsuarioCQRS)
            .WithName("CrearUsuarioCQRS")
            .Produces(201)
            .Produces(400);

        cqrsGroup.MapGet("/{id}", ObtenerUsuarioCQRS)
            .WithName("ObtenerUsuarioCQRS")
            .Produces(200)
            .Produces(404);

        cqrsGroup.MapGet("/", ListarUsuariosCQRS)
            .WithName("ListarUsuariosCQRS")
            .Produces(200);

        // Grupo Use Cases (v2)
        var useCasesGroup = endpoints.MapGroup("/api/v2/usuarios")
            .WithTags("Usuarios (Use Cases)");

        useCasesGroup.MapPost("/", CrearUsuarioUseCase)
            .WithName("CrearUsuarioV2")
            .Produces(201)
            .Produces(400);

        // Grupo Legacy
        var legacyGroup = endpoints.MapGroup("/api/usuarios")
            .WithTags("Usuarios (Legacy)");

        legacyGroup.MapPost("/", CrearUsuarioLegacy)
            .WithName("CrearUsuario")
            .Produces<UsuarioResponseDto>(201)
            .Produces(400);

        return endpoints;
    }

    private static async Task<IResult> CrearUsuarioCQRS(
        CrearUsuarioCommand command,
        ICommandHandler<CrearUsuarioCommand, CrearUsuarioCommandResponse> handler)
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

        return Results.Created($"/api/v3/usuarios/{response.UsuarioId}", new
        {
            id = response.UsuarioId,
            nombre = response.Nombre,
            email = response.Email,
            edadCalculada = response.EdadCalculada,
            fechaRegistro = response.FechaRegistro,
            mensaje = response.Mensaje
        });
    }

    private static async Task<IResult> ObtenerUsuarioCQRS(
        string id,
        IQueryHandler<ObtenerUsuarioQuery, ObtenerUsuarioQueryResponse> handler)
    {
        var query = new ObtenerUsuarioQuery { UsuarioId = id };
        var response = await handler.HandleAsync(query);

        if (!response.Success)
        {
            return Results.NotFound(new { mensaje = response.Mensaje });
        }

        return Results.Ok(response);
    }

    private static async Task<IResult> ListarUsuariosCQRS(
        int? Pagina,
        int? TamanoPagina,
        string? Filtro,
        IQueryHandler<ListarUsuariosQuery, ListarUsuariosQueryResponse> handler)
    {
        var query = new ListarUsuariosQuery
        {
            Pagina = Pagina ?? 1,
            TamanoPagina = TamanoPagina ?? 10,
            Filtro = Filtro
        };
        var response = await handler.HandleAsync(query);
        return Results.Ok(response);
    }

    private static async Task<IResult> CrearUsuarioUseCase(
        CrearUsuarioRequest request,
        ICrearUsuarioUseCase useCase)
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

        return Results.Created($"/api/v2/usuarios/{response.UsuarioId}", new
        {
            id = response.UsuarioId,
            nombre = response.Nombre,
            email = response.Email,
            edadCalculada = response.EdadCalculada,
            fechaRegistro = response.FechaRegistro,
            mensaje = response.Mensaje
        });
    }

    private static async Task<IResult> CrearUsuarioLegacy(
        UsuarioRequestDto request,
        IUsuarioService usuarioService)
    {
        var (success, response, validation) = await usuarioService.CrearUsuarioAsync(request);

        if (!success)
        {
            return Results.BadRequest(new
            {
                mensaje = "Errores de validación",
                validacion = validation,
                timestamp = DateTime.UtcNow
            });
        }

        return Results.Created($"/api/usuarios/{response!.Id}", response);
    }
}
