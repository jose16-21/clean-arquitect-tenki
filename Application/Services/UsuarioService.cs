using FluentValidation;
using HolaMundoNet10.Application.DTOs;

namespace HolaMundoNet10.Application.Services;

public interface IUsuarioService
{
    Task<(bool success, UsuarioResponseDto? response, ValidationResponseDto? validation)> CrearUsuarioAsync(UsuarioRequestDto request);
}

public class UsuarioService : IUsuarioService
{
    private readonly IValidator<UsuarioRequestDto> _validator;

    public UsuarioService(IValidator<UsuarioRequestDto> validator)
    {
        _validator = validator;
    }

    public async Task<(bool success, UsuarioResponseDto? response, ValidationResponseDto? validation)> CrearUsuarioAsync(UsuarioRequestDto request)
    {
        // Validar DTO directamente
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return (false, null, validationResult.ToDto());
        }

        // Mapear DTO a entidad
        var usuario = request.ToEntity();

        // Aquí iría la lógica para guardar en base de datos
        await Task.CompletedTask; // Simular operación async

        // Retornar resultado exitoso
        return (true, usuario.ToDto(), null);
    }
}
