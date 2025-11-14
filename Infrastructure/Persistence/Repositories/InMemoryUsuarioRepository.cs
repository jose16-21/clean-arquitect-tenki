using HolaMundoNet10.Domain.Entities;
using HolaMundoNet10.Infrastructure.Persistence.Repositories;

namespace HolaMundoNet10.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación en memoria del repositorio de Usuario
/// En producción, esto usaría Entity Framework o Dapper
/// </summary>
public class InMemoryUsuarioRepository : IUsuarioRepository
{
    private readonly List<Usuario> _usuarios = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<Usuario> CrearAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            _usuarios.Add(usuario);
            return await Task.FromResult(usuario);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Usuario?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return await Task.FromResult(_usuarios.FirstOrDefault(u => u.Id == id));
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<Usuario>> ListarAsync(int pagina, int tamanoPagina, string? filtro = null, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            var query = _usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(u =>
                    u.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(filtro, StringComparison.OrdinalIgnoreCase));
            }

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

    public async Task<int> ContarAsync(string? filtro = null, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            var query = _usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(u =>
                    u.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(filtro, StringComparison.OrdinalIgnoreCase));
            }

            return await Task.FromResult(query.Count());
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Usuario> ActualizarAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            var existente = _usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            if (existente != null)
            {
                _usuarios.Remove(existente);
                _usuarios.Add(usuario);
            }
            return await Task.FromResult(usuario);
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
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                _usuarios.Remove(usuario);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return await Task.FromResult(_usuarios.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
