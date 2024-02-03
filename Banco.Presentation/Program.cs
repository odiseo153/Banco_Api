using AutoMapper;
using Banco.Application.Queries;
using Banco.Infraestructure.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

BenchmarkDotNet.Running.BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly())
    .Run(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Banco_DBContext>();


builder.Services.AddScoped<UsuariosController>();
builder.Services.AddScoped<ClientesController>();
builder.Services.AddScoped<CuentasBancariaController>();
builder.Services.AddScoped<PrestamosController>();
builder.Services.AddScoped<TransaccioneController>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(op => op.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes odiseo es el mejor y lo sabes")),
        ValidateIssuerSigningKey = true

    });

builder.Services.AddAuthorization();


// Configuración de AutoMapper
var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
