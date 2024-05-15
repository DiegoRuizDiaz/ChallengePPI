namespace APIRest.Helpers

{
    //Middleware para manejar peticiones.
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);

                if (context.Response.StatusCode == 401)
                { 
                    await UnauthorizedMessage(context);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Se produjo un error interno del servidor. " + ex.Message);
            }
        }
        
        private async Task UnauthorizedMessage(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var customResponse = new
            {
                status = 401,
                message = "No Autorizado. Por favor provea credenciales válidas."
            };
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(customResponse));
        }               
    }
}

