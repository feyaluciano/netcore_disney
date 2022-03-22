using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APID.Dtos
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Activo { get; set; }
    }
}