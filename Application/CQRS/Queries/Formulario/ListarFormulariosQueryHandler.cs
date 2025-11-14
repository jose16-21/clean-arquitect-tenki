using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Queries.Formulario;

/// <summary>
/// Handler para el query ListarFormularios
/// </summary>
public class ListarFormulariosQueryHandler : IQueryHandler<ListarFormulariosQuery, ListarFormulariosQueryResponse>
{
    // En una aplicación real, inyectaríamos el repositorio aquí
    // private readonly IFormularioRepository _repository;

    public async Task<ListarFormulariosQueryResponse> HandleAsync(
        ListarFormulariosQuery query, 
        CancellationToken cancellationToken = default)
    {
        // Simulamos la búsqueda en la base de datos
        // En producción, esto vendría de un repositorio
        await Task.CompletedTask;

        // Datos simulados
        var formularios = new List<FormularioDto>
        {
            new FormularioDto 
            { 
                Id = Guid.NewGuid().ToString(), 
                Titulo = "Encuesta de Satisfacción", 
                Categoria = "encuestas",
                PrecioFinal = 45.00m,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(30),
                FechaCreacion = DateTime.UtcNow 
            },
            new FormularioDto 
            { 
                Id = Guid.NewGuid().ToString(), 
                Titulo = "Formulario de Registro", 
                Categoria = "registros",
                PrecioFinal = 30.00m,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(60),
                FechaCreacion = DateTime.UtcNow 
            },
            new FormularioDto 
            { 
                Id = Guid.NewGuid().ToString(), 
                Titulo = "Evaluación de Desempeño", 
                Categoria = "evaluaciones",
                PrecioFinal = 75.00m,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(90),
                FechaCreacion = DateTime.UtcNow 
            }
        };

        // Aplicar filtro por categoría si existe
        if (!string.IsNullOrEmpty(query.Categoria))
        {
            formularios = formularios
                .Where(f => f.Categoria?.Equals(query.Categoria, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }

        var total = formularios.Count;
        var totalPaginas = (int)Math.Ceiling(total / (double)query.TamanoPagina);

        // Aplicar paginación
        formularios = formularios
            .Skip((query.Pagina - 1) * query.TamanoPagina)
            .Take(query.TamanoPagina)
            .ToList();

        return new ListarFormulariosQueryResponse
        {
            Success = true,
            Formularios = formularios,
            Total = total,
            Pagina = query.Pagina,
            TotalPaginas = totalPaginas,
            Mensaje = $"Se encontraron {total} formularios (CQRS)"
        };
    }
}
