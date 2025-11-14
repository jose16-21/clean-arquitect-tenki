using FluentValidation;
using HolaMundoNet10.Application.DTOs;

namespace HolaMundoNet10.Application.Validators;

public class UsuarioValidator : AbstractValidator<UsuarioRequestDto>
{
    public UsuarioValidator()
    {
        // Validar nombre
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        // Validar email
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no tiene un formato válido");

        // Validar edad
        RuleFor(x => x.Edad)
            .GreaterThanOrEqualTo(18).WithMessage("La edad debe ser mayor o igual a 18")
            .LessThanOrEqualTo(120).WithMessage("La edad debe ser menor o igual a 120");

        // Validar salario
        RuleFor(x => x.Salario)
            .GreaterThan(0).WithMessage("El salario debe ser mayor a 0");
        
        RuleFor(x => x.Salario)
            .LessThan(10000000).WithMessage("El salario parece inusualmente alto")
            .WithSeverity(Severity.Warning);

        // Validar fecha de nacimiento
        RuleFor(x => x.FechaNacimiento)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de nacimiento no puede ser futura")
            .GreaterThan(new DateTime(1900, 1, 1)).WithMessage("La fecha de nacimiento no puede ser anterior a 1900");

        // Validar coherencia entre edad y fecha de nacimiento
        RuleFor(x => x)
            .Must(x => ValidarCoherenciaEdad(x.Edad, x.FechaNacimiento))
            .WithMessage(x => $"La edad proporcionada ({x.Edad}) no coincide con la fecha de nacimiento")
            .WithName("Coherencia Edad")
            .WithSeverity(Severity.Warning);

        // Validar que esté activo
        RuleFor(x => x.Activo)
            .Equal(true).WithMessage("El usuario debe estar activo para registrarse");

        // Validar teléfono (opcional)
        When(x => !string.IsNullOrWhiteSpace(x.Telefono), () =>
        {
            RuleFor(x => x.Telefono)
                .Must(telefono => LimpiarTelefono(telefono!).Length >= 10)
                .WithMessage("El teléfono debe tener al menos 10 dígitos");
        });

        // Validar roles
        RuleFor(x => x.Roles)
            .NotEmpty().WithMessage("Debe tener al menos un rol asignado");
        
        RuleFor(x => x.Roles)
            .Must(roles => roles.Length <= 5)
            .WithMessage("Se recomienda no asignar más de 5 roles")
            .WithSeverity(Severity.Warning);

        // Validar metadata
        When(x => x.Metadata != null && x.Metadata.ContainsKey("nivel"), () =>
        {
            RuleFor(x => x.Metadata!["nivel"])
                .Must(nivel => EsNivelValido(nivel))
                .WithMessage("El nivel en metadata debe estar entre 1 y 10");
        });
    }

    private bool ValidarCoherenciaEdad(int edad, DateTime fechaNacimiento)
    {
        var edadCalculada = DateTime.Now.Year - fechaNacimiento.Year;
        if (fechaNacimiento.Date > DateTime.Now.AddYears(-edadCalculada))
            edadCalculada--;

        return Math.Abs(edadCalculada - edad) <= 1;
    }

    private string LimpiarTelefono(string telefono)
    {
        return telefono.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "");
    }

    private bool EsNivelValido(object nivel)
    {
        if (int.TryParse(nivel.ToString(), out int nivelInt))
        {
            return nivelInt >= 1 && nivelInt <= 10;
        }
        return false;
    }
}
