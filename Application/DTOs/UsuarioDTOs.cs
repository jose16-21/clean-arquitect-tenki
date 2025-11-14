using HolaMundoNet10.Domain.Entities;
using HolaMundoNet10.Domain.Validators;

namespace HolaMundoNet10.Application.DTOs;

public record UsuarioRequestDto(
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

public record UsuarioResponseDto(
    Guid Id,
    string Nombre,
    string Email,
    int Edad,
    int EdadCalculada,
    decimal Salario,
    DateTime FechaNacimiento,
    bool Activo,
    string Telefono,
    string[] Roles,
    DateTime FechaRegistro,
    string Mensaje
);

public static class UsuarioMapper
{
    public static Usuario ToEntity(this UsuarioRequestDto dto)
    {
        return new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = dto.Nombre,
            Email = dto.Email,
            Edad = dto.Edad,
            Salario = dto.Salario,
            FechaNacimiento = dto.FechaNacimiento,
            Activo = dto.Activo,
            Telefono = dto.Telefono,
            Roles = dto.Roles?.ToList() ?? new List<string>(),
            Metadata = dto.Metadata,
            FechaRegistro = DateTime.UtcNow
        };
    }

    public static UsuarioResponseDto ToDto(this Usuario usuario)
    {
        return new UsuarioResponseDto(
            Id: usuario.Id,
            Nombre: usuario.Nombre,
            Email: usuario.Email,
            Edad: usuario.Edad,
            EdadCalculada: usuario.CalcularEdad(),
            Salario: usuario.Salario,
            FechaNacimiento: usuario.FechaNacimiento,
            Activo: usuario.Activo,
            Telefono: usuario.Telefono ?? "No proporcionado",
            Roles: usuario.Roles.ToArray(),
            FechaRegistro: usuario.FechaRegistro,
            Mensaje: "Usuario procesado correctamente"
        );
    }
}
