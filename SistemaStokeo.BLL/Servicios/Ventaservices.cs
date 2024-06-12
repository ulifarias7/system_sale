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
using Microsoft.EntityFrameworkCore;

namespace SistemaStokeo.BLL.Servicios
{

    // repasar esto de los servicios
    public class Ventaservices : IVentaservices
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<DetalleVenta> _detallerepositorio;
        private readonly IMapper _mapper;

        public Ventaservices(IVentaRepository ventaRepository, IGenericRepository<DetalleVenta> detallerepositorio, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _detallerepositorio = detallerepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDto> RegistrarVenta(VentaDto modelo)
        {
            try
            {
                var ventagenerada = await _ventaRepository.Registrar(_mapper.Map<Venta>(modelo));
                if (ventagenerada.IdVenta == 0) 
                    throw new TaskCanceledException(" no se creo la venta");

                return _mapper.Map<VentaDto>(ventagenerada);


            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VentaDto>> Historial(string Buscarpor, string Numerodeventa, string fechadeinicio, string fechadefin)
        {
            IQueryable<Venta> query = await _ventaRepository.Consultar();
            var listaResultado = new List<Venta>();
            try
            {
                if(Buscarpor == "fecha")
                {
                    DateTime fech_Inicio = DateTime.ParseExact(fechadeinicio, "dd/MM/yyyy", new CultureInfo("es_PE"));
                    DateTime fech_Fin = DateTime.ParseExact(fechadefin, "dd/MM/yyyy", new CultureInfo("es_PE"));

                    listaResultado = await query.Where(v=> 
                    v.FechaRegistro.Value.Date >=fech_Inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_Fin.Date
                    ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p=>p.IdProductoNavigation)
                    .ToListAsync();
                    
                }
                else
                {
                    listaResultado = await query.Where(v => v.NumeroDocumento == Numerodeventa)
                        .Include(dv=>dv.DetalleVenta)
                        .ThenInclude(p=>p.IdProductoNavigation)
                        .ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<VentaDto>>(listaResultado);
        }


        public async Task<List<ReporteDto>> Reporte(string fechadeinicio, string fechadefin)
        {
            IQueryable<DetalleVenta> query = await _detallerepositorio.Consultar();
            var listaResultado = new List<DetalleVenta>();
            try
            {
                DateTime fech_Inicio = DateTime.ParseExact(fechadeinicio, "dd/MM/yyyy", new CultureInfo("es_PE"));
                DateTime fech_Fin = DateTime.ParseExact(fechadefin, "dd/MM/yyyy", new CultureInfo("es_PE"));

                listaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                               dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date).ToListAsync();

            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReporteDto>>(listaResultado);
        }
    }
}
