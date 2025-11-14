namespace HolaMundoNet10.Application.UseCases.Formulario;

public interface ICrearFormularioUseCase
{
    Task<CrearFormularioResponse> ExecuteAsync(CrearFormularioRequest request);
}
