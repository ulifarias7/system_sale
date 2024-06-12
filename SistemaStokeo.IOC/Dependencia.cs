using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaStokeo.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaStokeo.DAL.Repositorios.Contratos;
using SistemaStokeo.DAL.Repositorios;
using SistemaStokeo.UTILITYS;
using AutoMapper;

namespace SistemaStokeo.IOC
{
    public static class Dependencia
    {

        public static void InyectarDependencias(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<DbsystemSContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });


            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository,VentaRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));





        }


    }
}
