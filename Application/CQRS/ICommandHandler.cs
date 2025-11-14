namespace HolaMundoNet10.Application.CQRS;

/// <summary>
/// Interfaz para handlers de Commands en CQRS
/// </summary>
/// <typeparam name="TCommand">Tipo de command a manejar</typeparam>
/// <typeparam name="TResponse">Tipo de respuesta del command</typeparam>
public interface ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
