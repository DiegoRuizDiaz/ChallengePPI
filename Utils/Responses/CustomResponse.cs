namespace Utils.Responses
{
    //Esta clase se utiliza para:
    //Manejar Respuestas HTTP.
    //Devolver distintas excepciones en llamadas de operaciones como PUT o DELETE.
    //El Dictionary se para la doc. de Swagger
    public class CustomResponse
    {
        public Dictionary<int, string> StatusCodeMessage { get; set; }

        public CustomResponse()
        {
            StatusCodeMessage = new Dictionary<int, string>
            {
                { 200, "Devuelve el recurso solicitado." },
                { 201, "El recurso se creó exitosamente." },
                { 204, "La solicitud se completó exitosamente" },
                { 400, "La solicitud enviada no es válida" },
                { 401, "Se requiere autenticación para acceder al recurso" },
                { 404, "El recurso no se ha encontrado" },
                { 500, "Error interno al procesar la solicitud" }
            };
        }       
    }
}

