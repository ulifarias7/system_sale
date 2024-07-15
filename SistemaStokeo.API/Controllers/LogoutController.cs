using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaStokeo.BLL.Servicios.Contrato;
using SistemaStokeo.UTILITYS;

namespace SistemaStokeo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LogoutController : ControllerBase
    {


        private readonly IUsuarioServices _Usuarioservices;
        private readonly Cryptoo _crypto;

        public LogoutController(IUsuarioServices usuarioservices, Cryptoo crypto)
        {
            _Usuarioservices = usuarioservices;
            _crypto = crypto;
        }

        [HttpPost]
        [Route("logout")]

        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _crypto.InvalidarToken(token);
            return Ok(new { mensaje = "Sesión cerrada exitosamente." });
        }


    }
}
