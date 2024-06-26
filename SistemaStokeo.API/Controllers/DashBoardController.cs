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
    public class DashBoardController : ControllerBase
    {

        private readonly IDashBoardservices _dashBoardservices;

        public DashBoardController(IDashBoardservices dashBoardservices)
        {
            _dashBoardservices = dashBoardservices;
        }



        //metodo de listar (resumen)

        [HttpGet]
        [Route("Resumen")]

        public async Task<IActionResult> Resumen()
        {
            var Rsp = new Response<DashBoardDto>();
            try
            {
                Rsp.status = true;
                Rsp.Value = await _dashBoardservices.Resumen();

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
