
using Domain.Enums;

namespace Domain.Models
{
    public class ActivosDTO
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Nombre { get; set; }
        public TiposActivosEnum TipoActivo { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
