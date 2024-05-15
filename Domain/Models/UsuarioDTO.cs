using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = " Debe ingresar un Nombre. ")]
        [DefaultValue("admin")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = " Debe ingresar un Email. ")]
        [DefaultValue("admin@gmail.com")]
        public string Email { get; set; }  
    }
}
