using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementation
{
    public class AuthService : IAuthService
    {
        private const string USUARIO_ADMIN= "ADMIN";
        private const string EMAIL_ADMIN = "ADMIN@GMAIL.COM";
        private readonly IConfiguration _config;
        
        public AuthService(IConfiguration config) 
        {
            this._config = config;
        }

        public async Task<string?> GetUsuarioToken(UsuarioDTO usuarioDTO)
        {
            string token = null;
            if( (string.Equals(usuarioDTO.Nombre.ToUpper(),USUARIO_ADMIN) && string.Equals(usuarioDTO.Email.ToUpper(), EMAIL_ADMIN)))
            {
                token = await GenerateToken(usuarioDTO);
            }
            return token;
        }

        private async Task<string> GenerateToken(UsuarioDTO usuarioDTO)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuarioDTO.Nombre),
                new Claim(ClaimTypes.Email,usuarioDTO.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

            var tokenSecurity = new JwtSecurityToken( claims: claims, expires : DateTime.Now.AddMinutes(50), signingCredentials: credenciales);

            string token = new JwtSecurityTokenHandler().WriteToken(tokenSecurity);
            
            return "bearer " + token;
        }
    }
}
