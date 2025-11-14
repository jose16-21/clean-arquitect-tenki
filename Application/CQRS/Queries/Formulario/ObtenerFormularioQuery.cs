using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Queries.Formulario;

/// <summary>
/// Query para obtener un formulario por ID
/// </summary>
public record ObtenerFormularioQuery : IQuery<ObtenerFormularioQueryResponse>
{
    public string FormularioId { get; init; } = string.Empty;
}

/// <summary>
/// Respuesta del query ObtenerFormulario
/// </summary>
public record ObtenerFormularioQueryResponse
{
    public bool Success { get; init; }
    public string? Id { get; init; }
    public string? Titulo { get; init; }
    public string? Descripcion { get; init; }
    public int? Cantidad { get; init; }
    public decimal? Precio { get; init; }
    public decimal? PrecioFinal { get; init; }
    public double? Descuento { get; init; }
    public DateTime? FechaInicio { get; init; }
    public DateTime? FechaFin { get; init; }
    public DateTime? FechaCreacion { get; init; }
    public string[]? Etiquetas { get; init; }
    public string? Mensaje { get; init; }
}
