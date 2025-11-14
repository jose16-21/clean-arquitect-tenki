namespace HolaMundoNet10.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Edad { get; set; }
    public decimal Salario { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public bool Activo { get; set; }
    public string? Telefono { get; set; }
    public List<string> Roles { get; set; } = new();
    public Dictionary<string, object>? Metadata { get; set; }
    public DateTime FechaRegistro { get; set; }

    public int CalcularEdad()
    {
        var edad = DateTime.Now.Year - FechaNacimiento.Year;
        if (FechaNacimiento.Date > DateTime.Now.AddYears(-edad))
            edad--;
        return edad;
    }
}
