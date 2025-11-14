namespace HolaMundoNet10.Application.CQRS;

/// <summary>
/// Interfaz para handlers de Queries en CQRS
/// </summary>
/// <typeparam name="TQuery">Tipo de query a manejar</typeparam>
/// <typeparam name="TResponse">Tipo de respuesta del query</typeparam>
public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
