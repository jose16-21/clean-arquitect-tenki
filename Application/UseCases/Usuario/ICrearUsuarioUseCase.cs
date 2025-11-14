namespace HolaMundoNet10.Application.UseCases.Usuario;

public interface ICrearUsuarioUseCase
{
    Task<CrearUsuarioResponse> ExecuteAsync(CrearUsuarioRequest request);
}
