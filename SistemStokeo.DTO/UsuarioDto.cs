using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemStokeo.DTO
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Correo { get; set; }

        public int? IdRol { get; set; }

        public string? RolDescripcion { get; set; }

        public string? Clave { get; set; }

        public int? EsActivo { get; set; } //no se usa como en el modelo el bool por que a la vista del usuario se trbaja con cero y uno por eso el entero 0 / 1 
    }
}
