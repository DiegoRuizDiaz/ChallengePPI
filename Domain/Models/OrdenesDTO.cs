using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class OrdenesDTO
    {
        public int? IdOrden { get; set; }
        public int? IdCuenta { get; set; }
        public int? IdActivo { get; set; }
        public string? NombreActivo { get; set; }        
        public int Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public OperacionEnum? Operacion { get; set; }
        public EstadosEnum Estado { get; set; }
        public decimal MontoTotal { get; set; }        
    }
}
