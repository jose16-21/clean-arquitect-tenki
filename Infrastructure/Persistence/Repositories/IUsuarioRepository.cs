using HolaMundoNet10.Domain.Entities;

namespace HolaMundoNet10.Infrastructure.Persistence.Repositories;

/// <summary>
/// Interfaz para repositorio de Usuario
/// </summary>
public interface IUsuarioRepository
{
    Task<Usuario> CrearAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task<Usuario?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Usuario>> ListarAsync(int pagina, int tamanoPagina, string? filtro = null, CancellationToken cancellationToken = default);
    Task<int> ContarAsync(string? filtro = null, CancellationToken cancellationToken = default);
    Task<Usuario> ActualizarAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default);
}
