using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaStokeo.DAL.DBContext;
using SistemaStokeo.DAL.Repositorios.Contratos;
using SistemaStokeo.DAL.Repositorios;
using SistemaStokeo.UTILITYS;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.BLL.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace SistemaStokeo.IOC
{
    public static class Dependencia
    {

        public static void InyectarDependencias(this IServiceCollection services,IConfiguration configuration)
        {
            //connection con la base de datos
            services.AddDbContext<DbsystemSContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });



            //services de encriptacion
            services.AddSingleton<Cryptoo>();

            services.AddAuthentication(config => {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });



            //repositorios
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository,VentaRepository>();

            //automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            //services
            services.AddScoped<IRolServices,RolServices>();
            services.AddScoped<ICategoriaServices,CategoriaServices>();
            services.AddScoped<IUsuarioServices,Usuarioservices>();
            services.AddScoped<IProductoServices,ProductoServices>();
            services.AddScoped<IVentaservices,Ventaservices>();
            services.AddScoped<IDashBoardservices,DashBoardServices>();
            services.AddScoped<IMenuServices,MenuServices>();


        }


    }
}
