using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APID.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APID.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PersonajeController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        private protected IConfiguration _configuration;

        public PersonajeController(AplicationDbContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();

            this._configuration=configuration;
        }

        [HttpGet]
        [Route("characters")]
        public  async Task<ActionResult<List<PersonajeDto>>> Personajes()
        {

            try
            {
                List<Personaje> Personajes = await _context.Personajes
                .Include(c => c.VideosFilm)
                .ToListAsync();
                if (Personajes == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No existen Personajes";
                    return Ok(_response);
                }
                _response.IsSuccess = true;
                _response.Message = "Personajes encontrados con éxito";
                List<PersonajeDto> PersonajesDto = _mapper.Map<List<Personaje>, List<PersonajeDto>>(Personajes);
                _response.Result = PersonajesDto;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al buscar el Personajes:" + ex.Message;
                return BadRequest(_response);
            }
        }


    }
}