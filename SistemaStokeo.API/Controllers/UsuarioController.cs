using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.API.Utilidad;
using SistemStokeo.DTO;
using SistemaStokeo.BLL.Servicios;


namespace SistemaStokeo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioServices _Usuarioservices;

        public UsuarioController(IUsuarioServices usuarioservices)
        {
            _Usuarioservices = usuarioservices;
        }


        //metodo de listar usuario

        [HttpGet]
        [Route("ListaUsuario")]

        public async Task<IActionResult> ListaUsuario()
        {
            var Rsp = new Response<List<UsuarioDto>>();
            try
            {
                Rsp.status = true;
                Rsp.Value = await _Usuarioservices.Lista();

            }
            catch (Exception ex)
            {
                Rsp.status = false;
                Rsp.Msg = ex.Message;

            }

            return Ok(Rsp);
        }

        //metodo para poner las credenciales

        [HttpPost]
        [Route("IniciarSesion")]

        public async Task<IActionResult> InicioSesion([FromBody] LoginDtos login)
        {
            var Rsp = new Response<SesionDto>();
            try
            {
                Rsp.status = true;
                Rsp.Value = await _Usuarioservices.ValidarCredenciales(login.Correo, login.Clave);

            }
            catch (Exception ex)
            {
                Rsp.status = false;
                Rsp.Msg = ex.Message;

            }

            return Ok(Rsp);
        }

        //metodo de Guardar/crear usuario

        [HttpPost]
        [Route("GuardarUsuario")]

        public async Task<IActionResult> GuardarUsuario([FromBody] UsuarioDto usuario)
        {
            var Rsp = new Response<UsuarioDto>();
            try
            {
                Rsp.status = true;
                Rsp.Value = await _Usuarioservices.Crear(usuario);

            }
            catch (Exception ex)
            {
                Rsp.status = false;
                Rsp.Msg = ex.Message;

            }

            return Ok(Rsp);
        }

        //metodo de editar Usuario

        [HttpPut]
        [Route("EditarUsuario")]

        public async Task<IActionResult>EditarUsuario(UsuarioDto modelo)
        {

            var editarusuario = new Response<bool>();
            try
            {
                editarusuario.status = true;
                editarusuario.Value = await _Usuarioservices.Editar(modelo);

            }
            catch (Exception ex)
            {
                editarusuario.status = false;
                editarusuario.Msg = ex.Message;

            }

            return Ok(editarusuario);


        }


        //metodo de eliminar usuario

        [HttpDelete]
        [Route("EliminarUsuario/{id:int}")]

        public async Task<IActionResult> EliminarUsuario(int id)
        {

            var eliminarUsuario = new Response<bool>();
            try
            {
                eliminarUsuario.status = true;
                eliminarUsuario.Value = await _Usuarioservices.Eliminar(id);

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
