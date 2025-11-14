using HolaMundoNet10.Domain.Entities;

namespace HolaMundoNet10.Application.DTOs;

public record FormularioRequestDto(
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

public record FormularioResponseDto(
    Guid Id,
    string Titulo,
    string? Descripcion,
    int Cantidad,
    decimal Precio,
    decimal PrecioFinal,
    double Descuento,
    DateTime FechaInicio,
    DateTime FechaFin,
    bool RequiereAprobacion,
    string? AprobadorEmail,
    string[] Etiquetas,
    DateTime FechaCreacion,
    string Mensaje
);

public static class FormularioMapper
{
    public static Formulario ToEntity(this FormularioRequestDto dto)
    {
        return new Formulario
        {
            Id = Guid.NewGuid(),
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Cantidad = dto.Cantidad,
            Precio = dto.Precio,
            Descuento = dto.Descuento,
            FechaInicio = dto.FechaInicio,
            FechaFin = dto.FechaFin,
            RequiereAprobacion = dto.RequiereAprobacion,
            AprobadorEmail = dto.AprobadorEmail,
            Etiquetas = dto.Etiquetas?.ToList() ?? new List<string>(),
            FechaCreacion = DateTime.UtcNow
        };
    }

    public static FormularioResponseDto ToDto(this Formulario formulario)
    {
        return new FormularioResponseDto(
            Id: formulario.Id,
            Titulo: formulario.Titulo,
            Descripcion: formulario.Descripcion,
            Cantidad: formulario.Cantidad,
            Precio: formulario.Precio,
            PrecioFinal: formulario.CalcularPrecioFinal(),
            Descuento: formulario.Descuento,
            FechaInicio: formulario.FechaInicio,
            FechaFin: formulario.FechaFin,
            RequiereAprobacion: formulario.RequiereAprobacion,
            AprobadorEmail: formulario.AprobadorEmail,
            Etiquetas: formulario.Etiquetas.ToArray(),
            FechaCreacion: formulario.FechaCreacion,
            Mensaje: "Formulario procesado correctamente"
        );
    }
}
