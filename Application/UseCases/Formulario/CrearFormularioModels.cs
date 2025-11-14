namespace HolaMundoNet10.Application.UseCases.Formulario;

public record CrearFormularioRequest(
    string Titulo,
    string? Descripcion,
    int Cantidad,
    decimal Precio,
    double Descuento,
    DateTime FechaInicio,
    DateTime FechaFin,
    bool RequiereAprobacion,
    string? AprobadorEmail,
    string[]? Etiquetas
);

public record CrearFormularioResponse(
    bool Success,
    Guid? FormularioId,
    string? Titulo,
    decimal? PrecioFinal,
    DateTime? FechaCreacion,
    string? Mensaje,
    List<string>? Errores,
    List<string>? Advertencias
);
