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
    [Route("api/user")]
    public class UsuarioController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        private protected IConfiguration _configuration;


        public UsuarioController(AplicationDbContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();

            this._configuration=configuration;
        }

        [HttpGet]
        [Route("getAll")]
        public  async Task<ActionResult<List<UsuarioDto>>> Usuarios()
        {

            try
            {
                List<Usuario> Usuarios = await _context.Usuarios.ToListAsync();
                if (Usuarios == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No existen Usuarios";
                    return Ok(_response);
                }
                
                _response.IsSuccess = true;
                _response.Message = "Usuarios encontrados con éxito";
                List<UsuarioDto> UsuariosDto = _mapper.Map<List<Usuario>, List<UsuarioDto>>(Usuarios);
                _response.Result = UsuariosDto;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al buscar el Usuarios:" + ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<UsuarioDto>> PostUsuario(UsuarioDto usuarioDto)
        {                       
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

            var usuarioValidator = new UsuarioValidator();
            var validationResult = await usuarioValidator.ValidateAsync(usuario);
            if (!validationResult.IsValid){
                  _response.IsSuccess = false;
                  _response.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
                  return NotFound(_response);               

            }


            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {               
                    _response.IsSuccess = false;
                    _response.Message = "Error al crear el usuario.";
                    return NotFound(_response);                    
               
            }
            _response.IsSuccess = true;
            _response.Message = "El usuario se ha creado con éxito.";
            _response.Result=_mapper.Map<UsuarioDto>(usuario);                           
            return Ok(_response);
        }


    }
}