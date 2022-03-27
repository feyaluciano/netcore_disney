using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;


namespace Infrastructure.Validators
{
    public class VideoFilmValidator : AbstractValidator<VideoFilm>
    {
        public VideoFilmValidator()
        {
            RuleFor(videofilm => videofilm.Titulo).NotNull().NotEmpty().Length(1,50).WithMessage("Ingrese un nombre entre 1 y 50 letras");
        }
    }
}