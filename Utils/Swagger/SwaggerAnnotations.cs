using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Utils.Responses;

namespace Utils.Swagger
{
    //Con esta clase se maneja la documentacion de Swagger.
    public class SwaggerAnnotations : IOperationFilter
    {
        private readonly CustomResponse _customResponse;
        public SwaggerAnnotations(CustomResponse responseResult)
        {
            _customResponse = responseResult;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //Elimino el response 200 que viene por def.
            var responses = operation.Responses;
            responses.Remove("200");

            switch (operation.OperationId)
            {
                case "Authenticate":
                    operation.Summary = "Authenticate";
                    operation.Description = "Devuelve un Bearer Token.";
                    responses.Add("200", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[200] });
                    responses.Add("400", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[400] });
                    responses.Add("404", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[404] });
                    responses.Add("500", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[500] });                
                break;

                case "ObtenerOrdenes":
                    operation.Summary = "Obtener órdenes";
                    operation.Description = "Retorna una lista de las órdenes existentes.";
                    responses.Add("200", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[200] });
                    responses.Add("404", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[404] });
                    responses.Add("500", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[500] });
                break;

                case "ObtenerOrden":
                    operation.Summary = "Obtener orden";
                    operation.Description = "Retorna una orden existente.";
                    responses.Add("200", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[200] });
                    responses.Add("404", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[404] });
                    responses.Add("500", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[500] });
                break;

                case "ActualizarOrden":
                    operation.Summary = "Actualizar orden";
                    operation.Description = "Actualiza el estado de una orden existente.";
                    responses.Add("204", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[201] });
                    responses.Add("404", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[404] });
                    responses.Add("500", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[500] });
                break;

                case "CrearOrden":
                    operation.Summary = "Crear orden.";
                    operation.Description = "Crea una nueva orden.";
                    responses.Add("201", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[201] });
                    responses.Add("400", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[400] });
                    responses.Add("500", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[500] });
                break;

                case "EliminarOrden":
                    operation.Summary = "Eliminar orden.";
                    operation.Description = "Elimina una orden existente.";
                    responses.Add("204", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[204] });
                    responses.Add("404", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[404] });
                    responses.Add("500", new OpenApiResponse { Description = _customResponse.StatusCodeMessage[500] });
                break;
                default:
                break;
            }                          
        }        
    }

}
