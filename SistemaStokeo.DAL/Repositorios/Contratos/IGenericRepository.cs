using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SistemaStokeo.DAL.Repositorios.Contratos
{
    public interface IGenericRepository <T> where T : class
    {

        Task<T> Obtener(Expression<Func<T, bool>> filtro);
        Task<T> Crear(T modelo);
        Task<bool> Editar(T modelo);
        Task<bool> Delete(T modelo);

        Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro = null);

    }
}
