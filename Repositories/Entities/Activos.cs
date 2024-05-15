using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Repositories.Entities
{
    public class Activos
    {
        [Key]
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Nombre { get; set; }
        public int TipoActivo { get; set; }
        public decimal PrecioUnitario { get; set; }
        
    }
}
