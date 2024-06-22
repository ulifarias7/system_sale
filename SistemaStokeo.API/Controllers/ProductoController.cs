using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemaStokeo.BLL.Servicios;
using SistemaStokeo.API.Utilidad;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.MODELS;
using SistemStokeo.DTO;

namespace SistemaStokeo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _productoservices;

        public ProductoController(IProductoServices productoservices)
        {
            _productoservices = productoservices;
        }

        [HttpGet]
        [Route("ListaProducto")]

        public async Task<IActionResult> ListaProducto()
        {
            var Rsp = new Response<List<ProductoDto>>();
            try
            {
                Rsp.status = true;
                Rsp.Value = await _productoservices.Lista();

            }
            catch (Exception ex)
            {
                Rsp.status = false;
                Rsp.Msg = ex.Message;

            }

            return Ok(Rsp);
        }


        [HttpPost]
        [Route("Crearproducto")]

        public async Task<ProductoDto> CrearProducto(ProductoDto producto)
        {

            var Producto = new Response<ProductoDto>();
            try
            {
                Producto.status = true;
                Producto.Value = await _productoservices.Crear(producto);

            }
            catch (Exception ex)
            {
                Producto.status = false;
                Producto.Msg = ex.Message;

            }

            return producto;
            

        }







    }
}
