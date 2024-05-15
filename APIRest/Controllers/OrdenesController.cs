using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Utils.Responses;

namespace APIRest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenesService _iOrdenesService;
        private readonly CustomResponse _customResponse;

        public OrdenesController(IOrdenesService iOrdenesService, CustomResponse customResponse)
        {
            _iOrdenesService = iOrdenesService;
            _customResponse = customResponse;
        }

        //Comienzo Metodos CRUD
        [SwaggerOperation(OperationId = "ObtenerOrdenes")]
        [HttpGet("ordenes")]
        public async Task<ActionResult<List<OrdenesDTO>>> GetAll()
        {
            try
            {
                var getOrdenes = await this._iOrdenesService.GetAll();

                if (getOrdenes.Count <= 0)
                {
                    return NotFound(_customResponse.StatusCodeMessage[404]);
                }
                    
                return Ok(getOrdenes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _customResponse.StatusCodeMessage[500] + " " + ex.Message);
            }
        }

        [SwaggerOperation(OperationId = "ObtenerOrden")]    
        [HttpGet("orden/{ordenId}")]
        public async Task<ActionResult<OrdenesDTO>> GetById([FromRoute] int ordenId)
        {
            try
            {
                var getOrden = await this._iOrdenesService.GetByOrdenId(ordenId);

                if (getOrden == null)
                {
                    return NotFound(_customResponse.StatusCodeMessage[404]);
                }

                return Ok(getOrden);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _customResponse.StatusCodeMessage[500] + " " + ex.Message);
            }
        }

        [SwaggerOperation(OperationId = "ActualizarOrden")] 
        [HttpPut("orden/{ordenId}/estado/{estado}")]
        public async Task<ActionResult> Update([FromRoute] int ordenId, [FromRoute] EstadosEnum estado)
        {
            try
            {  
                var ordenDTO = await this._iOrdenesService.Update(ordenId, estado);

                if (!ordenDTO)
                {
                    return NotFound(_customResponse.StatusCodeMessage[404]);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, _customResponse.StatusCodeMessage[500] + " " + ex.Message);
            }
        }

        [SwaggerOperation(OperationId = "CrearOrden")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrdenesRequestDTO ordenReqDTO)
        {
            try
            {
                var ordenDTO = await this._iOrdenesService.Post(ordenReqDTO);
                             
                if (ordenDTO == null)
                {
                    return NotFound(_customResponse.StatusCodeMessage[404]);
                }   

                return StatusCode(201, ordenDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _customResponse.StatusCodeMessage[500] + " : " + ex.Message);
            }
        }

        
        [SwaggerOperation(OperationId = "EliminarOrden")]
        [HttpDelete("{ordenId}")]
        public async Task<ActionResult> Delete([FromRoute] int ordenId)
        {
            try
            {
                var ordenDTO = await this._iOrdenesService.Delete(ordenId);
                if (!ordenDTO)
                {
                    return NotFound(_customResponse.StatusCodeMessage[404]);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, _customResponse.StatusCodeMessage[500] + " " + ex.Message);
            }
        }
        //Fin de Metodos CRUD
    }
}
