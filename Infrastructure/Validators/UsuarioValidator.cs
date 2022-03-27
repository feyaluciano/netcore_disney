using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;


namespace Infrastructure.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(genero => genero.Email).NotNull().NotEmpty().Length(6,10).WithMessage("Ingrese un nombre entre 6 y 10 caracteres");
        }
    }
}