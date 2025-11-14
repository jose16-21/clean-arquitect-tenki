using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Queries.Usuario;

/// <summary>
/// Query para obtener un usuario por ID
/// </summary>
public record ObtenerUsuarioQuery : IQuery<ObtenerUsuarioQueryResponse>
{
    public string UsuarioId { get; init; } = string.Empty;
}

/// <summary>
/// Respuesta del query ObtenerUsuario
/// </summary>
public record ObtenerUsuarioQueryResponse
{
    public bool Success { get; init; }
    public string? Id { get; init; }
    public string? Nombre { get; init; }
    public string? Email { get; init; }
    public int? Edad { get; init; }
    public decimal? Salario { get; init; }
    public DateTime? FechaNacimiento { get; init; }
    public bool? Activo { get; init; }
    public string? Telefono { get; init; }
    public string[]? Roles { get; init; }
    public DateTime? FechaRegistro { get; init; }
    public string? Mensaje { get; init; }
}
