using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemStokeo.DTO;

namespace SistemaStokeo.BLL.Servicios.Contrato
{
    public interface IProductoServices
    {
        Task<List<ProductoDto>> Lista();
        Task<ProductoDto> Crear(ProductoDto modelo);
        Task<bool> Editar(ProductoDto modelo);
        Task<bool> Eliminar(int id);
    }
}
