#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

public class Plato
{
    [Key]
    public int PlatoId { get; set; }
    [Required(ErrorMessage = "El Nombre es requerido.")]
    [MinLength(3)]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El Nombre del Chef es requerido.")]
    [MinLength(3)]
    public string Chef { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Seleccione un Numero entre 1 y 5.")]
    public int Sabor { get; set; }

    [Required(ErrorMessage = "Ingresa las Calorias.")]
    [Range(1, 5000, ErrorMessage = "Ingrese un Numero entre 1 y 5000.")]
    public int Calorias { get; set; }

    [Required(ErrorMessage = "Agrega una Descripcion.")]
    [MinLength(5, ErrorMessage = "Ingrese Descripcion")]
    public string Descripcion { get; set; }

    public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
    public DateTime Fecha_Modificacion { get; set; } = DateTime.Now;

}