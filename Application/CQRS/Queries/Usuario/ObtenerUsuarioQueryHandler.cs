using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Queries.Usuario;

/// <summary>
/// Handler para el query ObtenerUsuario
/// </summary>
public class ObtenerUsuarioQueryHandler : IQueryHandler<ObtenerUsuarioQuery, ObtenerUsuarioQueryResponse>
{
    // En una aplicación real, inyectaríamos el repositorio aquí
    // private readonly IUsuarioRepository _repository;

    public async Task<ObtenerUsuarioQueryResponse> HandleAsync(
        ObtenerUsuarioQuery query, 
        CancellationToken cancellationToken = default)
    {
        // Simulamos la búsqueda en la base de datos
        // En producción, esto vendría de un repositorio
        await Task.CompletedTask;

        // Por ahora retornamos datos simulados
        if (string.IsNullOrEmpty(query.UsuarioId))
        {
            return new ObtenerUsuarioQueryResponse
            {
                Success = false,
                Mensaje = "Usuario no encontrado"
            };
        }

        return new ObtenerUsuarioQueryResponse
        {
            Success = true,
            Id = query.UsuarioId,
            Nombre = "Usuario Ejemplo",
            Email = "usuario@example.com",
            Edad = 30,
            Salario = 50000,
            FechaNacimiento = new DateTime(1995, 1, 1),
            Activo = true,
            Telefono = "1234567890",
            Roles = new[] { "user" },
            FechaRegistro = DateTime.UtcNow,
            Mensaje = "Usuario encontrado (CQRS)"
        };
    }
}
