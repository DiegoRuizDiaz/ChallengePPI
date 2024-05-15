using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Repositories.Entities
{
    public class Ordenes
    {
        [Key]
        public int IdOrden { get; set; }
        public int IdCuenta { get; set; }
        public string NombreActivo { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public char Operacion { get; set; }
        public int Estado { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
