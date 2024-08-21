using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.DAL.Repositorios.Contratos;
using SistemStokeo.DTO;
using SistemaStokeo.MODELS;
using Microsoft.EntityFrameworkCore;


namespace SistemaStokeo.BLL.Servicios
{
    public class ProductoServices : IProductoServices
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoServices(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDto>> Listaproducto()
        {
            try
            {
                var queryProdcuto = await _productoRepository.Consultar();
                var listaProductos = queryProdcuto.Include(cat => cat.IdCategoriaNavigation).ToList();
                return _mapper.Map<List<ProductoDto>>(listaProductos.ToList());
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoDto> Crearproducto(ProductoDto modelo)
        {

            try
            {
                var ProductoCreado = await _productoRepository.Crear(_mapper.Map<Producto>(modelo));//convertimos producto 
                if (ProductoCreado.IdProducto == 0)
                    throw new TaskCanceledException(" no pudo ser creado");
                //var query = await _productoRepository.Consultar(u => u.IdProducto == ProductoCreado.);
                //ProductoCreado = query.Include(producto => producto.IdProductoNavigation).First();(idproductonavigation solo es para detalle venta)
                return _mapper.Map<ProductoDto>(ProductoCreado);
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> Editarproducto(ProductoDto modelo)
        {

            try
            {
                var productomodelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _productoRepository.Obtener(u => u.IdProducto == productomodelo.IdProducto);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("el producto no existe");


                productoEncontrado.Nombre = productomodelo.Nombre;
                productoEncontrado.IdCategoria= productomodelo.IdCategoria;
                productoEncontrado.Stock = productomodelo.Stock;
                productoEncontrado.Precio= productomodelo.Precio;
                productoEncontrado.EsActivo = productomodelo.EsActivo;

                bool respuesta = await _productoRepository.Editar(productoEncontrado);
                if (respuesta)
                    throw new TaskCanceledException("no se pudo editar");

                return respuesta;

            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> Eliminarproducto(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepository.Obtener(u => u.IdProducto == id);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("el producto  no existe ");

                bool respuesta = await _productoRepository.Delete(productoEncontrado);
                if (respuesta)
                    throw new TaskCanceledException("no se pudo editar");

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

    }
}
