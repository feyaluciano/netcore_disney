using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;


namespace Infrastructure.Validators
{
    public class GeneroValidator : AbstractValidator<Genero>
    {
        public GeneroValidator()
        {
            RuleFor(genero => genero.Nombre).NotNull().NotEmpty().Length(1,10).WithMessage("Ingrese un nombre entre 1 y 10 letras");
        }
    }
}