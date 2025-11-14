using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Queries.Usuario;

/// <summary>
/// Query para listar usuarios con paginación
/// </summary>
public record ListarUsuariosQuery : IQuery<ListarUsuariosQueryResponse>
{
    public int Pagina { get; init; } = 1;
    public int TamanoPagina { get; init; } = 10;
    public string? Filtro { get; init; }
}

/// <summary>
/// Respuesta del query ListarUsuarios
/// </summary>
public record ListarUsuariosQueryResponse
{
    public bool Success { get; init; }
    public List<UsuarioDto>? Usuarios { get; init; }
    public int Total { get; init; }
    public int Pagina { get; init; }
    public int TotalPaginas { get; init; }
    public string? Mensaje { get; init; }
}

/// <summary>
/// DTO para usuario en la lista
/// </summary>
public record UsuarioDto
{
    public string Id { get; init; } = string.Empty;
    public string Nombre { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public bool Activo { get; init; }
    public DateTime FechaRegistro { get; init; }
}
