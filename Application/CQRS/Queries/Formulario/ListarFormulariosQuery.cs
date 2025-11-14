using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Queries.Formulario;

/// <summary>
/// Query para listar formularios con paginación
/// </summary>
public record ListarFormulariosQuery : IQuery<ListarFormulariosQueryResponse>
{
    public int Pagina { get; init; } = 1;
    public int TamanoPagina { get; init; } = 10;
    public string? Categoria { get; init; }
    public bool? SoloActivos { get; init; }
}

/// <summary>
/// Respuesta del query ListarFormularios
/// </summary>
public record ListarFormulariosQueryResponse
{
    public bool Success { get; init; }
    public List<FormularioDto>? Formularios { get; init; }
    public int Total { get; init; }
    public int Pagina { get; init; }
    public int TotalPaginas { get; init; }
    public string? Mensaje { get; init; }
}

/// <summary>
/// DTO para formulario en la lista
/// </summary>
public record FormularioDto
{
    public string Id { get; init; } = string.Empty;
    public string Titulo { get; init; } = string.Empty;
    public string? Categoria { get; init; }
    public decimal PrecioFinal { get; init; }
    public DateTime FechaInicio { get; init; }
    public DateTime FechaFin { get; init; }
    public DateTime FechaCreacion { get; init; }
}
