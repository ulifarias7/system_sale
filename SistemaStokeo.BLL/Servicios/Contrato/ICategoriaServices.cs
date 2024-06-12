using SistemStokeo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStokeo.BLL.Servicios.Contrato
{
    public interface ICategoriaServices
    {
        Task<List<CategoriaDto>> List();
    }
}
