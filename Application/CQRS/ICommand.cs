namespace HolaMundoNet10.Application.CQRS;

/// <summary>
/// Interfaz base para Commands (operaciones de escritura) en CQRS
/// </summary>
/// <typeparam name="TResponse">Tipo de respuesta del command</typeparam>
public interface ICommand<TResponse>
{
}
