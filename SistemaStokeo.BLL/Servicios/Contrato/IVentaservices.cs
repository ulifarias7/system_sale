using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemStokeo.DTO;

namespace SistemaStokeo.BLL.Servicios.Contrato
{
    public interface IVentaservices
    {

        Task<VentaDto> RegistrarVenta(VentaDto modelo);
        Task<List<VentaDto>> Historial(string Buscarpor,string Numerodeventa,string fechadeinicio,string fechadefin);
        Task<List<ReporteDto>> Reporte(string fechadeinicio, string fechadefin);

        


    }
}
