using FluentValidation;
using HolaMundoNet10.Application.DTOs;

namespace HolaMundoNet10.Application.UseCases.Usuario;

public class CrearUsuarioUseCase : ICrearUsuarioUseCase
{
    private readonly IValidator<UsuarioRequestDto> _validator;

    public CrearUsuarioUseCase(IValidator<UsuarioRequestDto> validator)
    {
        _validator = validator;
    }

    public async Task<CrearUsuarioResponse> ExecuteAsync(CrearUsuarioRequest request)
    {
        // Mapear request a DTO para validación
        var dto = new UsuarioRequestDto(
            request.Nombre,
            request.Email,
            request.Edad,
            request.Salario,
            request.FechaNacimiento,
            request.Activo,
            request.Telefono,
            request.Roles,
            request.Metadata
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

            return new CrearUsuarioResponse(
                Success: false,
                UsuarioId: null,
                Nombre: null,
                Email: null,
                EdadCalculada: null,
                FechaRegistro: null,
                Mensaje: "Errores de validación",
                Errores: errores,
                Advertencias: advertencias
            );
        }

        // Crear entidad de dominio
        var usuario = dto.ToEntity();

        // Aquí iría la lógica de persistencia (repositorio)
        // Por ahora solo simulamos el guardado
        await Task.CompletedTask;

        // Retornar respuesta exitosa
        return new CrearUsuarioResponse(
            Success: true,
            UsuarioId: usuario.Id,
            Nombre: usuario.Nombre,
            Email: usuario.Email,
            EdadCalculada: usuario.CalcularEdad(),
            FechaRegistro: usuario.FechaRegistro,
            Mensaje: "Usuario creado exitosamente",
            Errores: null,
            Advertencias: null
        );
    }
}
