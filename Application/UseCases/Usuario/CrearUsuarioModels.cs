namespace HolaMundoNet10.Application.UseCases.Usuario;

public record CrearUsuarioRequest(
    string Nombre,
    string Email,
    int Edad,
    decimal Salario,
    DateTime FechaNacimiento,
    bool Activo,
    string? Telefono,
    string[] Roles,
    Dictionary<string, object>? Metadata
);

public record CrearUsuarioResponse(
    bool Success,
    Guid? UsuarioId,
    string? Nombre,
    string? Email,
    int? EdadCalculada,
    DateTime? FechaRegistro,
    string? Mensaje,
    List<string>? Errores,
    List<string>? Advertencias
);
