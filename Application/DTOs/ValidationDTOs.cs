using FluentValidation.Results;

namespace HolaMundoNet10.Application.DTOs;

public record ValidationResponseDto(
    bool EsValido,
    List<string> Errores,
    List<string> Advertencias,
    DateTime FechaValidacion
);

public static class ValidationMapper
{
    public static ValidationResponseDto ToDto(this ValidationResult result)
    {
        var errores = result.Errors
            .Where(e => e.Severity == FluentValidation.Severity.Error)
            .Select(e => e.ErrorMessage)
            .ToList();
            
        var advertencias = result.Errors
            .Where(e => e.Severity == FluentValidation.Severity.Warning)
            .Select(e => e.ErrorMessage)
            .ToList();
        
        return new ValidationResponseDto(
            EsValido: result.IsValid,
            Errores: errores,
            Advertencias: advertencias,
            FechaValidacion: DateTime.UtcNow
        );
    }
}
