using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaStokeo.MODELS;


namespace SistemaStokeo.DAL.Repositorios.Contratos
{
    public interface IVentaRepository : IGenericRepository<Venta>  //trabajamos con el modelo de venta 
    {

        Task<Venta> Registrar(Venta modelo); //metodo que devuelve una venta la cual se llama modelo

    }
}
