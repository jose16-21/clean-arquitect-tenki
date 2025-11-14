using HolaMundoNet10.Domain.Entities;

namespace HolaMundoNet10.Infrastructure.Persistence.Repositories;

/// <summary>
/// Interfaz para repositorio de Formulario
/// </summary>
public interface IFormularioRepository
{
    Task<Formulario> CrearAsync(Formulario formulario, CancellationToken cancellationToken = default);
    Task<Formulario?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Formulario>> ListarAsync(int pagina, int tamanoPagina, string? categoria = null, CancellationToken cancellationToken = default);
    Task<int> ContarAsync(string? categoria = null, CancellationToken cancellationToken = default);
    Task<Formulario> ActualizarAsync(Formulario formulario, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}
