namespace HolaMundoNet10.Domain.Entities;

public class Formulario
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public double Descuento { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public bool RequiereAprobacion { get; set; }
    public string? AprobadorEmail { get; set; }
    public List<string> Etiquetas { get; set; } = new();
    public DateTime FechaCreacion { get; set; }

    public decimal CalcularPrecioFinal()
    {
        var descuentoDecimal = (decimal)(Descuento / 100);
        return Precio - (Precio * descuentoDecimal);
    }
}
