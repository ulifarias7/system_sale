using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaStokeo.DAL.Repositorios.Contratos;
using SistemaStokeo.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SistemaStokeo.DAL.Repositorios
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly DbsystemSContext _dbContext;


        public GenericRepository(DbsystemSContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<T> Obtener(Expression<Func<T, bool>> filtro)
        {
            try
            {
                T modelo = await _dbContext.Set<T>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch
            {
                throw;
            }

        }

        public async Task<T> Crear(T modelo)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al guardar {typeof(T).Name}: {ex.Message}");
                throw;
            }
        }

        
        public async Task<bool> Editar(T modelo)
        {
            try
            {
                _dbContext.Set<T>().Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }

        }


        public async Task<bool> Delete(T modelo)
        {
            try
            {
                _dbContext.Set<T>().Remove(modelo);   //empezamos con la bdd ,establecemos el modelo que queremos trabajar 
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        

        public async Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro = null) // vamos a establecer  consultar para el que lo llame sea quien lo ejecute 

        {
            try
            {

                IQueryable<T> queryModelo = filtro == null ? _dbContext.Set<T>() : _dbContext.Set<T>().Where(filtro);
                return queryModelo.AsQueryable();
            }
            catch
            {
                throw;
            }

        }

       
       
      
      
    }
}
