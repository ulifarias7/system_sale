using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaStokeo.API.Utilidad;
using SistemaStokeo.IOC;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.InyectarDependencias(builder.Configuration);


//Cors para la conection
builder.Services.AddCors(options =>
{
    options.AddPolicy("Nuevapolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
}
);


var app = builder.Build();

//Prueba pusheo
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Nuevapolitica");

app.UseHttpsRedirection();
//jwt
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
