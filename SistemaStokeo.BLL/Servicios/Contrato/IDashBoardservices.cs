using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemStokeo.DTO;
namespace SistemaStokeo.BLL.Servicios.Contrato
{
    public interface IDashBoardservices
    {
        Task<DashBoardDto> Resumen();

    }
}
