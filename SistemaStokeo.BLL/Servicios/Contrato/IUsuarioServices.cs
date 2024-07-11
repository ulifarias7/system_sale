using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaStokeo.MODELS;
using SistemStokeo.DTO;

namespace SistemaStokeo.BLL.Servicios.Contrato
{
    public interface IUsuarioServices
    {
        Task<List<UsuarioDto>> Lista(); 
        Task<SesionDto> ValidarCredenciales(string correo, string clave);
        Task<UsuarioDto> Crear(UsuarioDto modelo);
        Task<bool>Editar(UsuarioDto modelo);
        Task<bool> Eliminar(int id);
    }
}
