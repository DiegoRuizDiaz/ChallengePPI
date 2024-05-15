using Utils.Enums;

namespace Services.Models
{
    public class OrdenesDTO : OrdenesRequestDTO
    {     
        public int? IdOrden { get; set; }
        public string? NombreActivo { get; set; }
        public EstadosEnum? Estado { get; set; }
        public decimal? MontoTotal { get; set; }
    }
}
