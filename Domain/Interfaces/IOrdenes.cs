namespace Domain.Interfaces
{

    public interface IOrdenes
    {
        decimal CalcularMontoTotal();
    }

    public class FCI : IOrdenes
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal CalcularMontoTotal()
        {
            return Cantidad * PrecioUnitario;
        }
    }

    public class Accion : IOrdenes
    {
        private const decimal IMPUESTO21 = 0.21m;
        private const decimal COMISION = 0.006m;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
       
        public decimal CalcularMontoTotal()
        {
            decimal montoTotal = Cantidad * PrecioUnitario;
            decimal comisiones = montoTotal * COMISION;
            decimal impuestos = comisiones * IMPUESTO21;
            return montoTotal + comisiones + impuestos;
        }
    }

    public class Bono : IOrdenes
    {
        private const decimal IMPUESTO21 = 0.21m;
        private const decimal COMISION = 0.002m;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        
        public decimal CalcularMontoTotal()
        {
            decimal montoTotal = Cantidad * Precio;
            decimal comisiones = montoTotal * COMISION;
            decimal impuestos = comisiones * IMPUESTO21;
            return montoTotal + comisiones + impuestos;
        }
    }
}
