using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APID.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APID.Controllers
{
    [ApiController]
    [Route("api/genero")]
    public class GeneroController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        private protected IConfiguration _configuration;


        public GeneroController(AplicationDbContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();

            this._configuration=configuration;
        }

        [HttpGet]
        [Route("getAll")]
        public  async Task<ActionResult<List<GeneroDto>>> Generos()
        {

            try
            {
                List<Genero> Generos = await _context.Generos.ToListAsync();
                if (Generos == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No existen Generos";
                    return Ok(_response);
                }
                
                _response.IsSuccess = true;
                _response.Message = "Generos encontrados con éxito";
                List<GeneroDto> GenerosDto = _mapper.Map<List<Genero>, List<GeneroDto>>(Generos);
                _response.Result = GenerosDto;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al buscar el Generos:" + ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<GeneroDto>> PostGenero(GeneroDto generoDto)
        {                       
            Genero genero = _mapper.Map<Genero>(generoDto);

            var generoValidator = new GeneroValidator();
            var validationResult = await generoValidator.ValidateAsync(genero);
            if (!validationResult.IsValid){
                  _response.IsSuccess = false;
                  _response.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
                  return NotFound(_response);               

            }


            _context.Generos.Add(genero);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {               
                    _response.IsSuccess = false;
                    _response.Message = "Error al crear el genero.";
                    return NotFound(_response);                    
               
            }
            _response.IsSuccess = true;
            _response.Message = "El genero se ha creado con éxito.";
            _response.Result=_mapper.Map<GeneroDto>(genero);                           
            return Ok(_response);
        }


    }
}