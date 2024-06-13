using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemStokeo.DTO;

namespace SistemaStokeo.BLL.Servicios.Contrato
{
    public interface IMenuServices
    {

        Task<List<MenuDto>> Lista(int IdUsuario);

    }
}
