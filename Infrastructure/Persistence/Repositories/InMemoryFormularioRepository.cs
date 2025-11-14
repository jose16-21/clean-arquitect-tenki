using HolaMundoNet10.Domain.Entities;
using HolaMundoNet10.Infrastructure.Persistence.Repositories;

namespace HolaMundoNet10.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación en memoria del repositorio de Formulario
/// En producción, esto usaría Entity Framework o Dapper
/// </summary>
public class InMemoryFormularioRepository : IFormularioRepository
{
    private readonly List<Formulario> _formularios = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<Formulario> CrearAsync(Formulario formulario, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            _formularios.Add(formulario);
            return await Task.FromResult(formulario);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Formulario?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return await Task.FromResult(_formularios.FirstOrDefault(f => f.Id == id));
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<Formulario>> ListarAsync(int pagina, int tamanoPagina, string? categoria = null, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            var query = _formularios.AsQueryable();

            // En una implementación real, Formulario tendría una propiedad Categoria
            // Por ahora solo devolvemos todos

            return await Task.FromResult(query
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList());
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<int> ContarAsync(string? categoria = null, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            var query = _formularios.AsQueryable();
            return await Task.FromResult(query.Count());
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Formulario> ActualizarAsync(Formulario formulario, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            var existente = _formularios.FirstOrDefault(f => f.Id == formulario.Id);
            if (existente != null)
            {
                _formularios.Remove(existente);
                _formularios.Add(formulario);
            }
            return await Task.FromResult(formulario);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            var formulario = _formularios.FirstOrDefault(f => f.Id == id);
            if (formulario != null)
            {
                _formularios.Remove(formulario);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
