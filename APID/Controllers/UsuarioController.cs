using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APID.Dtos;
using APID.Helpers;
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




        [HttpPost]
        [Route("auth/register")]
        public async Task<ActionResult<UsuarioDto>> Register(UsuarioDto usuarioDto, string password)
        {
            if (!EmailExists(usuarioDto.Email))
            {
                try
                {
                    Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
                    HashUtils.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                    usuario.PasswordHash = passwordHash;
                    usuario.PasswordSalt = passwordSalt;
                    await _context.Usuarios.AddAsync(usuario);
                    await _context.SaveChangesAsync();
                    _response.IsSuccess = true;
                    _response.Message = "El usuario fue registrado con éxito";
                    _response.Result = _mapper.Map<UsuarioDto>(usuario);
                    return Ok(_response);

                }
                catch (Exception ex)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Error al crear el usuario:" + ex.Message;
                    return BadRequest(_response);
                }
            }
            {
                _response.IsSuccess = false;
                _response.Message = "El email ya existe";
                return BadRequest(_response);
            }
        }

         [HttpPost]
        [Route("auth/login")]
        public async Task<ActionResult<string>> Login(UsuarioLoginDto usuario)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.ToLower() == usuario.Email);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Usuario o contraseña incorrecta";
                return BadRequest(_response);
            }
            if (!HashUtils.VerifyPasswordHash(usuario.Password, user.PasswordHash, user.PasswordSalt))
            {
                _response.IsSuccess = false;
                _response.Message = "Usuario o contraseña incorrecta";
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.Result=TokenUtils.GetToken(user,_configuration);
            _response.Message = "Inicio de sesion éxitoso";
            return Ok(_response);
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


        private bool EmailExists(string email)
        {
            return _context.Usuarios.Any(e => e.Email == email);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }


    }
}