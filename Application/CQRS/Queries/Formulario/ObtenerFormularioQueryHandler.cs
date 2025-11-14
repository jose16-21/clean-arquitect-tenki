using HolaMundoNet10.Application.CQRS;

namespace HolaMundoNet10.Application.CQRS.Queries.Formulario;

/// <summary>
/// Handler para el query ObtenerFormulario
/// </summary>
public class ObtenerFormularioQueryHandler : IQueryHandler<ObtenerFormularioQuery, ObtenerFormularioQueryResponse>
{
    // En una aplicación real, inyectaríamos el repositorio aquí
    // private readonly IFormularioRepository _repository;

    public async Task<ObtenerFormularioQueryResponse> HandleAsync(
        ObtenerFormularioQuery query, 
        CancellationToken cancellationToken = default)
    {
        // Simulamos la búsqueda en la base de datos
        // En producción, esto vendría de un repositorio
        await Task.CompletedTask;

        // Por ahora retornamos datos simulados
        if (string.IsNullOrEmpty(query.FormularioId))
        {
            return new ObtenerFormularioQueryResponse
            {
                Success = false,
                Mensaje = "Formulario no encontrado"
            };
        }

        return new ObtenerFormularioQueryResponse
        {
            Success = true,
            Id = query.FormularioId,
            Titulo = "Formulario Ejemplo",
            Descripcion = "Descripción del formulario",
            Cantidad = 100,
            Precio = 50.00m,
            PrecioFinal = 45.00m,
            Descuento = 10,
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddDays(30),
            FechaCreacion = DateTime.UtcNow,
            Etiquetas = new[] { "ejemplo", "test" },
            Mensaje = "Formulario encontrado (CQRS)"
        };
    }
}
