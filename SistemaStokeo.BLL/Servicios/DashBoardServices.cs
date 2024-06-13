using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.DAL.Repositorios.Contratos;
using SistemStokeo.DTO;
using SistemaStokeo.MODELS;
using System.Globalization;

namespace SistemaStokeo.BLL.Servicios
{
    public class DashBoardServices : IDashBoardservices
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<Producto> _Productorepositorio;
        private readonly IMapper _mapper;

        public DashBoardServices(IVentaRepository ventaRepository, IGenericRepository<Producto> productorepositorio, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _Productorepositorio = productorepositorio;
            _mapper = mapper;
        }

        //metodo solo para dashboardservices
        private IQueryable<Venta> RetornarVentas(IQueryable<Venta>tablaventa,int restarCantidadDias)
        {
            DateTime? ultimaFecha=tablaventa.OrderByDescending(v=>v.FechaRegistro).Select(v=>v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaventa.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }


        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaquery = await _ventaRepository.Consultar();

            if(_ventaquery.Count() > 0)
            {
                var tablaventa = RetornarVentas(_ventaquery, -7);
                total = tablaventa.Count();
            }
            return total;
        }

        private async Task<string> TotalIngresosUlitimasSemanas()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaquery = await _ventaRepository.Consultar();
            if(_ventaquery.Count()>0)
            {
                var tablaventa = RetornarVentas(_ventaquery, -7);
                resultado = tablaventa.Select(v => v.Total).Sum(v=>v.Value);
            }

            return Convert.ToString(resultado,new CultureInfo("es-PE"));
        }



        private async Task<int> TotalProducto()
        {
            IQueryable<Producto> _productoquery = await _Productorepositorio.Consultar();
            int total = _productoquery.Count();
            return total;
        }

        private async Task<Dictionary<string,int>> VentasUltimasSemanas()
        {
            Dictionary<string,int> resultado = new Dictionary<string,int>();
            IQueryable<Venta> _ventaquery = await _ventaRepository.Consultar();

            if(_ventaquery.Count()>0)
            {
                var tablaventa = RetornarVentas(_ventaquery, -7);
                resultado = tablaventa.GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r =>r.fecha, elementSelector: r => r.total);
            }

            return resultado;
        }

        public async Task<DashBoardDto> Resumen()
        {
            DashBoardDto dashboard = new DashBoardDto();
            try
            {
                dashboard.TotalVentas= await TotalVentasUltimaSemana();
                dashboard.TotalIngresos = await TotalIngresosUlitimasSemanas();
                dashboard.TotalProductos = await TotalProducto();
                

                List<VentasSemanaDto>listaventa = new List<VentasSemanaDto>();
                foreach(KeyValuePair<string,int>item in await VentasUltimasSemanas())
                {
                    listaventa.Add(new VentasSemanaDto()
                    {
                        Fecha = item.Key,
                        Total = item.Value, 

                    });
                }
                dashboard .VentasUltimaSemana = listaventa;
            }
            catch
            {
                throw;
            }

            return dashboard;
        }
    }
}
