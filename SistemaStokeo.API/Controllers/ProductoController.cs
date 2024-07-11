﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.API.Utilidad;
using SistemStokeo.DTO;
using SistemaStokeo.BLL.Servicios;
using Microsoft.AspNetCore.Authorization;

namespace SistemaStokeo.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly IProductoServices _productoservicio;

        public ProductoController(IProductoServices productoservicio)
        {
            _productoservicio = productoservicio;
        }


        //metodo de listar Producto

        [HttpGet]
        [Route("ListaProducto")]

        public async Task<IActionResult> ListaProducto()
        {
            var Rsp = new Response<List<ProductoDto>>();
            try
            {
                Rsp.status = true;
                Rsp.Value = await _productoservicio.Lista();

            }
            catch (Exception ex)
            {
                Rsp.status = false;
                Rsp.Msg = ex.Message;

            }

            return Ok(Rsp);
        }


        //metodo de Guardar/crear Producto

        [HttpPost]
        [Route("CrearProducto")]

        public async Task<IActionResult> CrearProducto([FromBody] ProductoDto producto)
        {
            var Rsp = new Response<ProductoDto>();
            try
            {
                Rsp.status = true;
                Rsp.Value = await _productoservicio.Crear(producto);

            }
            catch (Exception ex)
            {
                Rsp.status = false;
                Rsp.Msg = ex.Message;

            }

            return Ok(Rsp);
        }

        //metodo de editar Producto

        [HttpPut]
        [Route("EditarProducto")]

        public async Task<IActionResult> EditarProducto(ProductoDto Producto)
        {

            var editarProducto = new Response<bool>();
            try
            {
                editarProducto.status = true;
                editarProducto.Value = await _productoservicio.Editar(Producto);

            }
            catch (Exception ex)
            {
                editarProducto.status = false;
                editarProducto.Msg = ex.Message;

            }

            return Ok(editarProducto);


        }

        //metodo de eliminar usuario

        [HttpDelete]
        [Route("EliminarProducto/{id:int}")]

        public async Task<IActionResult> EliminarProducto(int id)
        {

            var eliminarUsuario = new Response<bool>();
            try
            {
                eliminarUsuario.status = true;
                eliminarUsuario.Value = await _productoservicio.Eliminar(id);

            }
            catch (Exception ex)
            {
                eliminarUsuario.status = false;
                eliminarUsuario.Msg = ex.Message;

            }

            return Ok(eliminarUsuario);


        }







    }
}
