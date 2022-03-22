using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APID.Dtos
{
    public class VideoFilmDto
    {
        public int IdVideoFilm { get; set; }


        public string Titulo { get; set; }

        public DateTime FechaDeCreacion { get; set; }


        public int Calificacion { get; set; }



        public ICollection<PersonajeDto> PersonajesAsociados { get; set; }
    }
}