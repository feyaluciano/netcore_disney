using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Core.Entities
{
    public class Usuario
    {
         [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [MaxLength(50), MinLength(4)]
        public string Email { get; set; }

       
        public byte[] PasswordHash { get; set; }

         public byte[] PasswordSalt { get; set; }

        [MaxLength(50), MinLength(1)]
        public string Nombre { get; set; }

        [MaxLength(50), MinLength(1)]
        public string Apellido { get; set; }

        public bool Activo { get; set; }

    }
}