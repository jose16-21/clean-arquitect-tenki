using FluentValidation;
using HolaMundoNet10.Application.DTOs;

namespace HolaMundoNet10.Application.Validators;

public class FormularioValidator : AbstractValidator<FormularioRequestDto>
{
    public FormularioValidator()
    {
        // Validar título
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("El título es obligatorio")
            .MinimumLength(3).WithMessage("El título debe tener al menos 3 caracteres")
            .MaximumLength(200).WithMessage("El título no puede exceder 200 caracteres");

        // Validar descripción (opcional)
        When(x => !string.IsNullOrWhiteSpace(x.Descripcion), () =>
        {
            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripción excede 500 caracteres")
                .WithSeverity(Severity.Warning);
        });

        // Validar cantidad
        RuleFor(x => x.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");
        
        RuleFor(x => x.Cantidad)
            .LessThan(10000).WithMessage("La cantidad parece inusualmente alta")
            .WithSeverity(Severity.Warning);

        // Validar precio
        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser positivo");
        
        RuleFor(x => x.Precio)
            .LessThan(1000000).WithMessage("El precio parece inusualmente alto")
            .WithSeverity(Severity.Warning);

        // Validar descuento
        RuleFor(x => x.Descuento)
            .InclusiveBetween(0, 100).WithMessage("El descuento debe estar entre 0 y 100");

        RuleFor(x => x.Descuento)
            .LessThanOrEqualTo(80).WithMessage("El descuento es muy alto (mayor a 80%)")
            .WithSeverity(Severity.Warning);

        // Validar fechas
        RuleFor(x => x.FechaInicio)
            .GreaterThanOrEqualTo(DateTime.Now.Date)
            .WithMessage("La fecha de inicio es anterior a hoy")
            .WithSeverity(Severity.Warning);

        RuleFor(x => x.FechaFin)
            .GreaterThan(x => x.FechaInicio)
            .WithMessage("La fecha de fin debe ser posterior a la fecha de inicio");

        // Validar duración
        RuleFor(x => x)
            .Must(x => (x.FechaFin - x.FechaInicio).TotalDays <= 365)
            .WithMessage("El periodo es mayor a un año")
            .WithSeverity(Severity.Warning)
            .WithName("Duración");

        // Validar email del aprobador cuando se requiere aprobación
        When(x => x.RequiereAprobacion, () =>
        {
            RuleFor(x => x.AprobadorEmail)
                .NotEmpty().WithMessage("El email del aprobador es obligatorio cuando se requiere aprobación")
                .EmailAddress().WithMessage("El email del aprobador no tiene un formato válido");
        });

        // Validar etiquetas
        When(x => x.Etiquetas == null || x.Etiquetas.Length == 0, () =>
        {
            RuleFor(x => x.Etiquetas)
                .NotEmpty()
                .WithMessage("No se proporcionaron etiquetas")
                .WithSeverity(Severity.Warning);
        });

        RuleFor(x => x.Etiquetas)
            .Must(etiquetas => etiquetas == null || etiquetas.Length <= 10)
            .WithMessage("Máximo 10 etiquetas permitidas");

        When(x => x.Etiquetas != null && x.Etiquetas.Length > 0, () =>
        {
            RuleFor(x => x.Etiquetas)
                .Must(etiquetas => etiquetas!.All(e => !string.IsNullOrWhiteSpace(e)))
                .WithMessage("Las etiquetas no pueden estar vacías");
        });

        // Validar lógica de negocio - precio final
        RuleFor(x => x)
            .Must(x => CalcularPrecioFinal(x.Precio, x.Descuento) > 0)
            .WithMessage("El precio final después del descuento no puede ser 0 o negativo")
            .WithName("Precio Final");
    }

    private decimal CalcularPrecioFinal(decimal precio, double descuento)
    {
        var descuentoDecimal = (decimal)(descuento / 100);
        return precio - (precio * descuentoDecimal);
    }
}
