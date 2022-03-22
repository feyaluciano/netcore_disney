using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace APID.Dtos
{
    public class PersonajeDto
    {
       // public int IdPersonaje { get; set; }
        
        public string Nombre { get; set; }

        public string Imagen { get; set; }

       // public int Edad { get; set; }
       
       // public Decimal Peso { get; set; }

       // public string Historia { get; set; }

        public ICollection<VideoFilmDto> VideosFilm { get; set; }
    }
}