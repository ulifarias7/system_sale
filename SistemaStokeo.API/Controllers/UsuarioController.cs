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


        [HttpPost]
        [Route("CrearUsuario")]

        public async Task<UsuarioDto> CrearUsuario(UsuarioDto modelo)
        {

            var NuevoUsuario = new Response<UsuarioDto>();
            try
            {
                NuevoUsuario.status = true;
                NuevoUsuario.Value = await _Usuarioservices.Crear(modelo);

            }
            catch (Exception ex)
            {
                NuevoUsuario.status = false;
                NuevoUsuario.Msg = ex.Message;

            }

            return modelo;


        }

        [HttpPut]
        [Route("EditarUsurio")]
        public async Task<IActionResult>EditarUsuario(UsuarioDto modelo)
        {
            var Ueliminado = new Response<UsuarioDto>();
            try
            {
                Ueliminado.status = true;
                Ueliminado.Value = await _Usuarioservices.Editar(modelo);

            }
            catch(Exception ex)
            {
                Ueliminado.status = false;
                Ueliminado.Msg = ex.Message;

            }
            return Ok(modelo); 
        }


        [HttpDelete]
        [Route("EliminarUsuario")]
        public async Task<bool> EliminarUsuario(int id)
        {

        }
    }
}
