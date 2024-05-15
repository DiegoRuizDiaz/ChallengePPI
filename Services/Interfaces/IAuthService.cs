using Domain.Models;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> GetUsuarioToken(UsuarioDTO usuarioDTO);
    }
}
