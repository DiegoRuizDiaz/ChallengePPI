using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    [EnumValidoParaUpdate]
    public enum EstadosEnum
    {
        EnProceso = 0,
        Ejecutada = 1,
        Cancelada = 2
    }

    public class EnumValidoParaUpdate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var tipo = (EstadosEnum)value;

            if (tipo == EstadosEnum.EnProceso)
            {
                return new ValidationResult("Una orden existente y en proceso solo puede ser Ejecutada o Cancelada");
            }

            return ValidationResult.Success;
        }
    }
}