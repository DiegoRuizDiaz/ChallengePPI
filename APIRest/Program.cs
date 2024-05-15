using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repositories.Data;
using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Implementation;
using Services.Interfaces;
using Services.Mapping;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Utils.Swagger;
using Utils.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using APIRest.Helpers;
using APIRest.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Servicios
        builder.Services.RegisterServices(builder);

        var app = builder.Build();
        app.UseMiddleware<GlobalExceptionMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "APIRest v1");
                s.RoutePrefix = "swagger";
            });
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

        });

        app.Run();
    }
}