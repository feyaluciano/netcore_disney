using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Personaje
    {
        [Key]
        [Column("IdPersonaje")]
        public int IdPersonaje { get; set; }

        [MaxLength(50), MinLength(1)]
        public string Nombre { get; set; }

        public string Imagen { get; set; }

        public int Edad { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public Decimal Peso { get; set; }

        public string Historia { get; set; }

        public ICollection<VideoFilm> VideosFilm { get; set; }
    }
}