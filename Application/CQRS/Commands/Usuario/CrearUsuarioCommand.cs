using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Commands.Usuario;

/// <summary>
/// Command para crear un nuevo usuario
/// </summary>
public record CrearUsuarioCommand : ICommand<CrearUsuarioCommandResponse>
{
    public string Nombre { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public int Edad { get; init; }
    public decimal Salario { get; init; }
    public DateTime FechaNacimiento { get; init; }
    public bool Activo { get; init; }
    public string? Telefono { get; init; }
    public string[] Roles { get; init; } = Array.Empty<string>();
    public Dictionary<string, object>? Metadata { get; init; }
}

/// <summary>
/// Respuesta del command CrearUsuario
/// </summary>
public record CrearUsuarioCommandResponse
{
    public bool Success { get; init; }
    public string? UsuarioId { get; init; }
    public string? Nombre { get; init; }
    public string? Email { get; init; }
    public int? EdadCalculada { get; init; }
    public DateTime? FechaRegistro { get; init; }
    public string Mensaje { get; init; } = string.Empty;
    public List<string>? Errores { get; init; }
    public List<string>? Advertencias { get; init; }
}
