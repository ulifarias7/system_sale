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
    public class MenuController : ControllerBase
    {
        private readonly IMenuServices _menuServices;

        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }



        [HttpGet]
        [Route("ListaMenu")]

        public async Task<IActionResult> ListaMenu(int IdUsuario)
        {
            var Rsp = new Response<List<MenuDto>>();
            try
            {
                Rsp.status = true;
                Rsp.Value = await _menuServices.Lista(IdUsuario);

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
