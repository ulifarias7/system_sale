using SistemaStokeo.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemStokeo.DTO
{
    public class DashBoardDto
    {
        public int TotalVentas { get; set; }

        public string? TotalIngresos { get; set; }

        public int TotalProductos { get; set; }
        public List<VentasSemanaDto> VentasUltimaSemana { get; set; }  


    }
}
