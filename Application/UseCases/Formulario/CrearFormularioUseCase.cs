using FluentValidation;
using HolaMundoNet10.Application.DTOs;

namespace HolaMundoNet10.Application.UseCases.Formulario;

public class CrearFormularioUseCase : ICrearFormularioUseCase
{
    private readonly IValidator<FormularioRequestDto> _validator;

    public CrearFormularioUseCase(IValidator<FormularioRequestDto> validator)
    {
        _validator = validator;
    }

    public async Task<CrearFormularioResponse> ExecuteAsync(CrearFormularioRequest request)
    {
        // Mapear request a DTO para validación
        var dto = new FormularioRequestDto(
            request.Titulo,
            request.Descripcion,
            request.Cantidad,
            request.Precio,
            request.Descuento,
            request.FechaInicio,
            request.FechaFin,
            request.RequiereAprobacion,
            request.AprobadorEmail,
            request.Etiquetas
        );

        // Validar usando FluentValidation
        var validationResult = await _validator.ValidateAsync(dto);

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

            return new CrearFormularioResponse(
                Success: false,
                FormularioId: null,
                Titulo: null,
                PrecioFinal: null,
                FechaCreacion: null,
                Mensaje: "Errores de validación",
                Errores: errores,
                Advertencias: advertencias
            );
        }

        // Crear entidad de dominio
        var formulario = dto.ToEntity();

        // Aquí iría la lógica de persistencia (repositorio)
        // Por ahora solo simulamos el guardado
        await Task.CompletedTask;

        // Retornar respuesta exitosa
        return new CrearFormularioResponse(
            Success: true,
            FormularioId: formulario.Id,
            Titulo: formulario.Titulo,
            PrecioFinal: formulario.CalcularPrecioFinal(),
            FechaCreacion: formulario.FechaCreacion,
            Mensaje: "Formulario creado exitosamente",
            Errores: null,
            Advertencias: null
        );
    }
}
