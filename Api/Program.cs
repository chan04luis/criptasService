using Api.HostedService;
using Business.Implementation;
using Business.Interfaces;
using Data.cs;
using Microsoft.EntityFrameworkCore;
using Utils.Implementation;
using Utils.Interfaces;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;
using Business.Data;
using Data.cs.Commands;
using Microsoft.Extensions.Options;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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

builder.Services.AddControllers();
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
        options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Iglesia API",
        Version = "v1",
        Description = "API para gestión de criptas."
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

#region Inyeccion de dependencias
builder.Services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddScoped<IBusClientes, BusClientes>();
builder.Services.AddScoped<IFiltros, Filtros>();
builder.Services.AddScoped<IIglesiasRepositorio, IglesiasRepositorio>();
builder.Services.AddScoped<IBusIglesias, BusIglesias>();
builder.Services.AddScoped<IZonasRepositorio, ZonasRepositorio>();
builder.Services.AddScoped<IBusZonas, BusZonas>();
builder.Services.AddScoped<ISeccionesRepositorio, SeccionesRepositorio>();
builder.Services.AddScoped<IBusSecciones, BusSecciones>();
builder.Services.AddScoped<ICriptasRepositorio, CriptasRepositorio>();
builder.Services.AddScoped<IBusCriptas, BusCriptas>();
builder.Services.AddScoped<ITiposPagoRepositorio, TiposDePagoRepositorio>();
builder.Services.AddScoped<IBusTiposPago, BusTiposPago>();
builder.Services.AddScoped<IPagosRepositorio, PagosRepositorio>();
builder.Services.AddScoped<IBusPagos, BusPagos>();
builder.Services.AddScoped<IPagosParcialesRepositorio, PagosParcialesRepositorio>();
builder.Services.AddScoped<IVisitasRepositorio, VisitasRepositorio>();
builder.Services.AddScoped<IBusVisitas, BusVisitas>();
builder.Services.AddScoped<IFallecidosRepositorio, FallecidosRepositorio>();
builder.Services.AddScoped<IBusFallecidos, BusFallecidos>();
builder.Services.AddScoped<IBeneficiariosRepositorio, BeneficiariosRepositorio>();
builder.Services.AddScoped<IBusBeneficiarios, BusBeneficiarios>();
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
