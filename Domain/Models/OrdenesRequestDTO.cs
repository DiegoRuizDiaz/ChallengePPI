using Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class OrdenesRequestDTO
    {
        [Required(ErrorMessage = "El campo IdCuenta es requerido.")]
        [DefaultValue(1)]
        public int? IdCuenta { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es requerido.")]
        [DefaultValue(1)]
        [Range(1, int.MaxValue, ErrorMessage = "El campo Cantidad no puede ser igual o menor a 0.")]
        public int? Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Precio es requerido.")]
        [Decimal(ErrorMessage = "El campo Precio no puede ser menor o igual a 0.")]
        [DefaultValue(20.56)]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo Operación es requerido.")]
        public OperacionEnum? Operacion { get; set; }


        //Validacion para Decimal
        public class DecimalAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value == null)
                    return true;

                decimal decimalValue;
                if (decimal.TryParse(value.ToString(), out decimalValue))
                {
                    //Valido si es negativo
                    if (decimalValue < 0)
                    {
                        return false;
                    }
                    //Valido si es igual a cero.
                    if (decimalValue == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }   
    }
}
