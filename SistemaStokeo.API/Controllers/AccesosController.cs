using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.API.Utilidad;
using SistemStokeo.DTO;
using SistemaStokeo.BLL.Servicios;
using SistemaStokeo.DAL.DBContext;
using SistemaStokeo.MODELS;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using SistemaStokeo.UTILITYS;

namespace SistemaStokeo.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]//estamos informando que esta api de acceso , vamos a poder acceder sin que el usuario se encuntre autorizado (ya que solo vamos a ingresar session y registrarnos)
    [ApiController]
    public class AccesosController : ControllerBase
    {
        private readonly IUsuarioServices _Usuarioservices;
        private readonly Cryptoo _crypto;

        public AccesosController(IUsuarioServices usuarioservices, Cryptoo crypto)
        {
            _Usuarioservices = usuarioservices;
            _crypto = crypto;
        }


        //metodo de Guardar/crear usuario

        [HttpPost]
        [Route("Registrarse")]

        public async Task<IActionResult> Registrarse([FromBody] UsuarioDto usuario)
        {
            var Rsp = new Response<UsuarioDto>();

            try
            {
                usuario.Clave = _crypto.encriptarSHA256(usuario.Clave);
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


        //metodo para poner las credenciales

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginDtos login)
        {
            var Rsp = new Response<SesionDto>();

            try
            {
                // Encriptar la clave proporcionada en el login
                var claveencriptada = _crypto.encriptarSHA256(login.Clave);
                var sesion = await _Usuarioservices.ValidarCredenciales(login.Correo, claveencriptada);

                if (sesion != null)
                {
                    var token = _crypto.generarJWt(sesion);
                    sesion.Token = token;
                    Rsp.status = true;
                    Rsp.Value = sesion;
                }
                else
                {
                    throw new Exception("El usuario no existe o la clave es incorrecta ");
                }
            }
            catch (Exception ex)
            {
                Rsp.status = false;
                Rsp.Msg = ex.Message;
            }

            return Ok(Rsp);
        }



    }
}

