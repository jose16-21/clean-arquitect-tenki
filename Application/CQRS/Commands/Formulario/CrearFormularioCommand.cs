using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Commands.Formulario;

/// <summary>
/// Command para crear un nuevo formulario
/// </summary>
public record CrearFormularioCommand : ICommand<CrearFormularioCommandResponse>
{
    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public int Cantidad { get; init; }
    public decimal Precio { get; init; }
    public double Descuento { get; init; }
    public DateTime FechaInicio { get; init; }
    public DateTime FechaFin { get; init; }
    public bool RequiereAprobacion { get; init; }
    public string? AprobadorEmail { get; init; }
    public string[]? Etiquetas { get; init; }
}

/// <summary>
/// Respuesta del command CrearFormulario
/// </summary>
public record CrearFormularioCommandResponse
{
    public bool Success { get; init; }
    public string? FormularioId { get; init; }
    public string? Titulo { get; init; }
    public decimal? PrecioFinal { get; init; }
    public DateTime? FechaCreacion { get; init; }
    public string Mensaje { get; init; } = string.Empty;
    public List<string>? Errores { get; init; }
    public List<string>? Advertencias { get; init; }
}
