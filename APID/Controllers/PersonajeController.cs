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



         [HttpGet]
        [Route("characters/search/")]public  async Task<ActionResult<List<PersonajeDto>>> Search([FromQuery]string name, [FromQuery]int age)
        {

            try
            {
                List<Personaje> Personajes = await _context.Personajes.Where(c => c.Nombre.Contains(name) || c.Edad==age)
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





        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<PersonajeDto>> PostPersonaje(PersonajeDto personajeDto)
        {                       
            Personaje personaje = _mapper.Map<Personaje>(personajeDto);
            _context.Personajes.Add(personaje);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {               
                    _response.IsSuccess = false;
                    _response.Message = "Error al crear el personaje.";
                    return NotFound(_response);                    
               
            }
            _response.IsSuccess = true;
            _response.Message = "El personaje se ha creado con éxito.";
            _response.Result=_mapper.Map<PersonajeDto>(personaje);                           
            return Ok(_response);
        }

         [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeletePersonaje(int id)
        {
            var personaje = await _context.Personajes.FindAsync(id);
            if (personaje == null)
            {
                _response.IsSuccess = false;
                _response.Message = "El personaje no éxiste.";
                return NotFound(_response);        
            }
            _context.Personajes.Remove(personaje);
            await _context.SaveChangesAsync();
            _response.IsSuccess = false;
            _response.Message = "El personaje se eliminó con éxito.";
            _response.Result=_mapper.Map<PersonajeDto>(personaje);                            
            return Ok(_response);
        }

         [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutPersonaje(int id, PersonajeDto personajeDto)
        {                       
             Personaje personaje = await _context.Personajes.FindAsync(id);

            if (id != personajeDto.IdPersonaje)
            {
                _response.IsSuccess = false;
                _response.Message = "El personaje no existe";
                return BadRequest(_response);
            }           
            personaje.Nombre = personajeDto.Nombre;            
            _context.Entry(personaje).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
                {
                    _response.IsSuccess = false;
                    _response.Message = "El personaje no existe.";
                    return NotFound(_response);                    
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Error al actualizar el personaje.";
                    return NotFound(_response);             
                }
            }
            
            _response.IsSuccess = true;
            _response.Message = "El personaje se ha modificado con éxito.";
            _response.Result=_mapper.Map<PersonajeDto>(personaje);                           
            return Ok(_response);
        }

         private bool PersonajeExists(int id)
        {
            return _context.Personajes.Any(e => e.IdPersonaje == id);
        }

    }
}