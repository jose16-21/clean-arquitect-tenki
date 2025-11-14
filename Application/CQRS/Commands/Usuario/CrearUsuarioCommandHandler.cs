using FluentValidation;
using HolaMundoNet10.Application.CQRS;
using HolaMundoNet10.Application.DTOs;

namespace HolaMundoNet10.Application.CQRS.Commands.Usuario;

/// <summary>
/// Handler para el command CrearUsuario
/// </summary>
public class CrearUsuarioCommandHandler : ICommandHandler<CrearUsuarioCommand, CrearUsuarioCommandResponse>
{
    private readonly IValidator<UsuarioRequestDto> _validator;

    public CrearUsuarioCommandHandler(IValidator<UsuarioRequestDto> validator)
    {
        _validator = validator;
    }

    public async Task<CrearUsuarioCommandResponse> HandleAsync(
        CrearUsuarioCommand command, 
        CancellationToken cancellationToken = default)
    {
        // Mapear command a DTO para validación
        var dto = new UsuarioRequestDto(
            command.Nombre,
            command.Email,
            command.Edad,
            command.Salario,
            command.FechaNacimiento,
            command.Activo,
            command.Telefono,
            command.Roles,
            command.Metadata
        );

        // Validar usando FluentValidation
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .Where(e => e.Severity == Severity.Error)
                .Select(e => e.ErrorMessage)
                .ToList();
                
            var advertencias = validationResult.Errors
                .Where(e => e.Severity == Severity.Warning)
                .Select(e => e.ErrorMessage)
                .ToList();

            return new CrearUsuarioCommandResponse
            {
                Success = false,
                Mensaje = "Errores de validación",
                Errores = errores,
                Advertencias = advertencias
            };
        }

        // Crear entidad de dominio
        var usuario = dto.ToEntity();

        // Aquí iría la lógica de persistencia (repositorio)
        // Por ahora solo simulamos el guardado
        await Task.CompletedTask;

        // Retornar respuesta exitosa
        return new CrearUsuarioCommandResponse
        {
            Success = true,
            UsuarioId = usuario.Id.ToString(),
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            EdadCalculada = usuario.CalcularEdad(),
            FechaRegistro = usuario.FechaRegistro,
            Mensaje = "Usuario creado exitosamente (CQRS)"
        };
    }
}
