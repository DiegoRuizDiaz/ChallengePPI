using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Utils.Responses;
using Domain.Models;


namespace APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _iAuthService;
        private readonly CustomResponse _customResponse;
        
        public AuthController(IAuthService iAuthService, CustomResponse customResponse)
        {
            _iAuthService = iAuthService;
            _customResponse = customResponse;
        }

        [AllowAnonymous]
        [SwaggerOperation(OperationId = "Authenticate")]
        [HttpPost("authenticate")]
        public async Task<ActionResult> Login(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuarioToken = await this._iAuthService.GetUsuarioToken(usuarioDTO);

                if (usuarioToken == null)
                {
                    return NotFound(_customResponse.StatusCodeMessage[404] + ". Credenciales Invalidas.");
                }
                return Ok(usuarioToken);              
            }
            catch (Exception ex)
            {
                return StatusCode(500, _customResponse.StatusCodeMessage[500] + " " + ex.Message);
            }                       
        }
    }
}
