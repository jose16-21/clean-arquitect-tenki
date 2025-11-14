namespace HolaMundoNet10.Application.CQRS;

/// <summary>
/// Interfaz base para Queries (operaciones de lectura) en CQRS
/// </summary>
/// <typeparam name="TResponse">Tipo de respuesta del query</typeparam>
public interface IQuery<TResponse>
{
}
