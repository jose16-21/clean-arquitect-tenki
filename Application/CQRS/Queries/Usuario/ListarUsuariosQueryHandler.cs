using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Queries.Usuario;

/// <summary>
/// Handler para el query ListarUsuarios
/// </summary>
public class ListarUsuariosQueryHandler : IQueryHandler<ListarUsuariosQuery, ListarUsuariosQueryResponse>
{
    // En una aplicación real, inyectaríamos el repositorio aquí
    // private readonly IUsuarioRepository _repository;

    public async Task<ListarUsuariosQueryResponse> HandleAsync(
        ListarUsuariosQuery query, 
        CancellationToken cancellationToken = default)
    {
        // Simulamos la búsqueda en la base de datos
        // En producción, esto vendría de un repositorio
        await Task.CompletedTask;

        // Datos simulados
        var usuarios = new List<UsuarioDto>
        {
            new UsuarioDto 
            { 
                Id = Guid.NewGuid().ToString(), 
                Nombre = "Juan Pérez", 
                Email = "juan@example.com", 
                Activo = true, 
                FechaRegistro = DateTime.UtcNow 
            },
            new UsuarioDto 
            { 
                Id = Guid.NewGuid().ToString(), 
                Nombre = "María García", 
                Email = "maria@example.com", 
                Activo = true, 
                FechaRegistro = DateTime.UtcNow 
            },
            new UsuarioDto 
            { 
                Id = Guid.NewGuid().ToString(), 
                Nombre = "Pedro López", 
                Email = "pedro@example.com", 
                Activo = false, 
                FechaRegistro = DateTime.UtcNow 
            }
        };

        // Aplicar filtro si existe
        if (!string.IsNullOrEmpty(query.Filtro))
        {
            usuarios = usuarios
                .Where(u => u.Nombre.Contains(query.Filtro, StringComparison.OrdinalIgnoreCase) ||
                           u.Email.Contains(query.Filtro, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        var total = usuarios.Count;
        var totalPaginas = (int)Math.Ceiling(total / (double)query.TamanoPagina);

        // Aplicar paginación
        usuarios = usuarios
            .Skip((query.Pagina - 1) * query.TamanoPagina)
            .Take(query.TamanoPagina)
            .ToList();

        return new ListarUsuariosQueryResponse
        {
            Success = true,
            Usuarios = usuarios,
            Total = total,
            Pagina = query.Pagina,
            TotalPaginas = totalPaginas,
            Mensaje = $"Se encontraron {total} usuarios (CQRS)"
        };
    }
}
