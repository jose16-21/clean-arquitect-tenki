using FluentValidation;
using HolaMundoNet10.Application.DTOs;

namespace HolaMundoNet10.Application.Services;

public interface IFormularioService
{
    Task<(bool success, FormularioResponseDto? response, ValidationResponseDto? validation)> CrearFormularioAsync(FormularioRequestDto request);
}

public class FormularioService : IFormularioService
{
    private readonly IValidator<FormularioRequestDto> _validator;

    public FormularioService(IValidator<FormularioRequestDto> validator)
    {
        _validator = validator;
    }

    public async Task<(bool success, FormularioResponseDto? response, ValidationResponseDto? validation)> CrearFormularioAsync(FormularioRequestDto request)
    {
        // Validar DTO directamente
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return (false, null, validationResult.ToDto());
        }

        // Mapear DTO a entidad
        var formulario = request.ToEntity();

        // Aquí iría la lógica para guardar en base de datos
        await Task.CompletedTask; // Simular operación async

        // Retornar resultado exitoso
        return (true, formulario.ToDto(), null);
    }
}
