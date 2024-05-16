using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Ordenes
    {
        [Key]
        public int IdOrden { get; set; }
        public int IdCuenta { get; set; }
        public int IdActivo { get; set; }
        public string NombreActivo { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public char Operacion { get; set; }
        public int Estado { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
