using Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class OrdenesRequestDTO
    {
        [Required(ErrorMessage = "El campo IdCuenta es requerido.")]
        [DefaultValue(1)]
        public int? IdCuenta { get; set; }

        [Required(ErrorMessage = "El campo Ticker es requerido.")]
        [DefaultValue("GD30")]
        public string Ticker { get; set; }

        [Required(ErrorMessage = "El campo Nombre Activo es requerido.")]
        [StringLength(32, ErrorMessage = "El Nombre Activo debe tener como maximo 32 caracteres.")]
        [DefaultValue("Inversion en Bonos Globales")]
        public string? NombreActivo { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es requerido.")]
        [DefaultValue(1)]
        [Range(1, int.MaxValue, ErrorMessage = "El campo Cantidad no puede ser igual o menor a 0.")]
        public int Cantidad { get; set; }

        [DefaultValue(1234.1234)]
        public decimal? Precio { get; set; }

        [Required(ErrorMessage = "El campo Operación es requerido.")]
        public OperacionEnum? Operacion { get; set; }
    }
}
