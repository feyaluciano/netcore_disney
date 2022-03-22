using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace APID.Dtos
{
    public class GeneroDto
    {
        
         public int IdGenero { get; set; }
      
        public string Nombre { get; set; }

        public string Imagen { get; set; }
        

        public virtual ICollection<VideoFilmDto> VideosFilm { get; set; }

    }
}