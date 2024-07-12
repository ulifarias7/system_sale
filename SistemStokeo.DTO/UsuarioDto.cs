using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemStokeo.DTO
{
    public class UsuarioDto
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public string? NombreCompleto { get; set; }
        [Required]
        public string? Correo { get; set; }
        [Required]
        public int? IdRol { get; set; }
        [Required]
        public string? Clave { get; set; }

        public int? EsActivo { get; set; } //no se usa como en el modelo el bool por que a la vista del usuario se trbaja con cero y uno por eso el entero 0 / 1

        public string? RolDescripcion { get; set; }
 
    }
}
