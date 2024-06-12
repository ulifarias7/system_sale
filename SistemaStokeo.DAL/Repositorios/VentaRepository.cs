using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaStokeo.DAL.DBContext;
using SistemaStokeo.DAL.Repositorios.Contratos;
using SistemaStokeo.MODELS;

namespace SistemaStokeo.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbsystemSContext _dbContext;

        public VentaRepository(DbsystemSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)//leer este metodo
        {
            Venta ventagenerada = new Venta();
            using (var trasaction = _dbContext.Database.BeginTransaction())
                try
                {
                    foreach(DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(producto_encontrado);
                    }
                    await _dbContext.SaveChangesAsync();

                    NumeroDocumento numeroDocumento = _dbContext.NumeroDocumentos.First();

                    numeroDocumento.UltimoNumero = numeroDocumento.UltimoNumero + 1;
                    numeroDocumento.FechaRegistro = DateTime.Now;

                    _dbContext.NumeroDocumentos.Update(numeroDocumento);
                    await _dbContext.SaveChangesAsync();

                    //00002 

                    int CantidaDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidaDigitos));
                    string numerodeVenta = ceros + numeroDocumento.UltimoNumero.ToString();
                    //00001

                    numerodeVenta = numerodeVenta.Substring(numerodeVenta.Length - CantidaDigitos, CantidaDigitos);
                    modelo.NumeroDocumento = numerodeVenta;

                    await _dbContext.Venta.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    ventagenerada = modelo;

                    trasaction.Commit();




                }
                catch
                {
                    trasaction.Rollback();
                    throw;
                }

            return ventagenerada;
        }
    }
}
