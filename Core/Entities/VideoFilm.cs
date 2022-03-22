using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Core.Entities
{
    public class VideoFilm
    {

        [Key]
        [Column("IdVideoFilm")]
        public int IdVideoFilm { get; set; }

        [MaxLength(50), MinLength(1)]
        public string? Titulo { get; set; }

        public DateTime FechaDeCreacion { get; set; }

        [MaxLength(5), MinLength(1)]   
        public int Calificacion { get; set; }

         public ICollection<Personaje>? PersonajesAsociados { get; set; }

        
    }
}