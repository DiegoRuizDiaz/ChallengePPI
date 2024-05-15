using APIRest.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Data;
using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Implementation;
using Services.Interfaces;
using Services.Mapping;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Utils.Responses;
using Utils.Swagger;

namespace APIRest.Extensions
{
    public static class ServiceRegistratonExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //Authentication Jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
            (options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false
                });

            //Usar seguridad en todos los endpoint.
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            //AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            //DbContext
            services.AddDbContext<AppDbContext>(
            options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
            });

            //Repositories
            services.AddScoped<IOrdenesRepository, OrdenesRepository>();
            services.AddScoped<IActivosRepository, ActivosRepository>();

            //Services
            services.AddScoped<IOrdenesService, OrdenesService>();
            services.AddScoped<IActivosService, ActivosService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<CustomResponse>();

            //Swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "APIRest", Version = "v1" });

                //Manejo las Annotations de Swagger con esta clase.
                s.EnableAnnotations();
                s.OperationFilter<SwaggerAnnotations>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                var esquemaSeguridad = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Ingrese la cadena devuelta por el metodo authenticate.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                };
                s.AddSecurityDefinition("Bearer", esquemaSeguridad);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }
                        ,new string[] { }
                    }
                };
                s.AddSecurityRequirement(securityRequirement);
            });

            builder.Services.AddEndpointsApiExplorer();

            //MiddleWare
            builder.Services.AddTransient<GlobalExceptionMiddleware>();

            return services;
        }
    }
}
