using FluentValidation;
using HolaMundoNet10.Application.CQRS;
using HolaMundoNet10.Application.DTOs;

namespace HolaMundoNet10.Application.CQRS.Commands.Formulario;

/// <summary>
/// Handler para el command CrearFormulario
/// </summary>
public class CrearFormularioCommandHandler : ICommandHandler<CrearFormularioCommand, CrearFormularioCommandResponse>
{
    private readonly IValidator<FormularioRequestDto> _validator;

    public CrearFormularioCommandHandler(IValidator<FormularioRequestDto> validator)
    {
        _validator = validator;
    }

    public async Task<CrearFormularioCommandResponse> HandleAsync(
        CrearFormularioCommand command, 
        CancellationToken cancellationToken = default)
    {
        // Mapear command a DTO para validación
        var dto = new FormularioRequestDto(
            command.Titulo,
            command.Descripcion,
            command.Cantidad,
            command.Precio,
            command.Descuento,
            command.FechaInicio,
            command.FechaFin,
            command.RequiereAprobacion,
            command.AprobadorEmail,
            command.Etiquetas
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

            return new CrearFormularioCommandResponse
            {
                Success = false,
                Mensaje = "Errores de validación",
                Errores = errores,
                Advertencias = advertencias
            };
        }

        // Crear entidad de dominio
        var formulario = dto.ToEntity();

        // Aquí iría la lógica de persistencia (repositorio)
        // Por ahora solo simulamos el guardado
        await Task.CompletedTask;

        // Retornar respuesta exitosa
        return new CrearFormularioCommandResponse
        {
            Success = true,
            FormularioId = formulario.Id.ToString(),
            Titulo = formulario.Titulo,
            PrecioFinal = formulario.CalcularPrecioFinal(),
            FechaCreacion = formulario.FechaCreacion,
            Mensaje = "Formulario creado exitosamente (CQRS)"
        };
    }
}
