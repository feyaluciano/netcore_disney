using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Core.Entities
{
    public class Genero
    {
        [Key]
        [Column("IdGenero")]
        public int IdGenero { get; set; }

        [MaxLength(50), MinLength(1)]
        public string Nombre { get; set; }

        public string Imagen { get; set; }
        

        //ESTO GENERAR UNA CLAVE EN VideoFilm
        public virtual ICollection<VideoFilm> VideosFilm { get; set; }

    }
}