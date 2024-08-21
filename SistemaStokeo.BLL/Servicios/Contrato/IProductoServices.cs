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
        Task<List<ProductoDto>> Listaproducto();
        Task<ProductoDto> Crearproducto(ProductoDto modelo);
        Task<bool> Editarproducto(ProductoDto modelo);
        Task<bool> Eliminarproducto(int id);
       
    }
}
