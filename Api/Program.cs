using Api.HostedService;
using Data.cs;
using Microsoft.EntityFrameworkCore;
using Utils.Implementation;
using Utils.Interfaces;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;
using Business.Data;
using Data.cs.Commands;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using FluentValidation;
using Data.cs.Interfaces.Seguridad;
using Data.cs.Commands.Seguridad;
using Business.Interfaces.Seguridad;
using Business.Implementation.Seguridad;
using Business.Implementation.Catalogos;
using Business.Interfaces.Catalogos;
using Models.Models;
using Models.Validations.Seguridad;

var builder = WebApplication.CreateBuilder(args);
#region JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });
#endregion
//builder.WebHost.UseUrls("http://127.0.0.1:5001");
// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configuracion detalles
var allowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin() // Permitir cualquier origen
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = null // Mantiene PascalCase
        };
    }); 



builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioUpdateValidator>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Gownetwork API",
        Version = "v1",
        Description = "API para gestión de Gownetwork."
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization - Ingresa el token por ejemplo: 'Bearer {token}'",
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header,
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    c.EnableAnnotations();
    c.SchemaFilter<SwaggerSchemaFilter>();
});

#endregion

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
string CONNECTION_STRING = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseNpgsql(CONNECTION_STRING);
});

/*string ISSUER = builder.Configuration["JwtSettings:Issuer"];
string JWT_SECRET_KEY = builder.Configuration["JwtSettings:SecretKey"];
builder.Services.AddScoped<BusJwt>(provider =>
{
    return new BusJwt(JWT_SECRET_KEY, ISSUER);
});*/

#region Inyeccion de dependencias Catalogo
builder.Services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddScoped<IBusClientes, BusClientes>();
builder.Services.AddScoped<IFiltros, Filtros>();
builder.Services.AddScoped<ISucursalesRepositorio, SucursalesRepositorio>();
builder.Services.AddScoped<IBusSucursal, BusSucursal>();

#endregion

#region Inyeccion de dependencias Seguridad

builder.Services.AddScoped<IBusAutenticacion, BusAutenticacion>();

builder.Services.AddScoped<IPerfilesRepositorio, PerfilesRepositorio>();
builder.Services.AddScoped<IBusPerfiles, BusPerfiles>();

builder.Services.AddScoped<IConfiguracionesRepositorio, ConfiguracionesRepositorio>();
builder.Services.AddScoped<IBusConfiguracion, BusConfiguracion>();

builder.Services.AddScoped<IBotonesRepositorio, BotonesRepositorio>();
builder.Services.AddScoped<IBusBoton, BusBoton>();

builder.Services.AddScoped<IPaginasRespositorio, PaginasRepositorio>();
builder.Services.AddScoped<IBusPagina, BusPagina>();

builder.Services.AddScoped<IModulosRepositorio, ModulosRepositorio>();
builder.Services.AddScoped<IBusModulo, BusModulo>();

builder.Services.AddScoped<IPermisosRepositorio, PermisosRepositorio>();
builder.Services.AddScoped<IBusPermiso, BusPermiso>();

builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddScoped<IBusUsuarios, BusUsuarios>();

#endregion

#region Hosted Background Services
//builder.Services.AddHostedService<TestDat>();
#endregion

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DefaultModelsExpandDepth(-1);
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});
app.UseCors(allowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
