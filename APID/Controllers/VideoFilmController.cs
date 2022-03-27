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
    [Route("api/videofilm")]
    public class VideoFilmController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        private protected IConfiguration _configuration;


        public VideoFilmController(AplicationDbContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();
            this._configuration=configuration;
        }

        [HttpGet]
        [Route("movies")]
        public  async Task<ActionResult<List<VideoFilmDto>>> VideoFilms()
        {

            try
            {
                List<VideoFilm> VideoFilms = await _context.VideosFilm.ToListAsync();
                if (VideoFilms == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No existen VideoFilms";
                    return Ok(_response);
                }
                
                _response.IsSuccess = true;
                _response.Message = "VideoFilms encontrados con éxito";
                List<VideoFilmDto> VideoFilmsDto = _mapper.Map<List<VideoFilm>, List<VideoFilmDto>>(VideoFilms);
                _response.Result = VideoFilmsDto;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al buscar el VideoFilms:" + ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<VideoFilmDto>> PostVideoFilm(VideoFilmDto videofilmDto)
        {                       
            VideoFilm videofilm = _mapper.Map<VideoFilm>(videofilmDto);

            var videofilmValidator = new VideoFilmValidator();
            var validationResult = await videofilmValidator.ValidateAsync(videofilm);
            if (!validationResult.IsValid){
                  _response.IsSuccess = false;
                  _response.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
                  return NotFound(_response);               

            }


            _context.VideosFilm.Add(videofilm);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {               
                    _response.IsSuccess = false;
                    _response.Message = "Error al crear el videofilm.";
                    return NotFound(_response);                    
               
            }
            _response.IsSuccess = true;
            _response.Message = "El videofilm se ha creado con éxito.";
            _response.Result=_mapper.Map<VideoFilmDto>(videofilm);                           
            return Ok(_response);
        }


    }
}