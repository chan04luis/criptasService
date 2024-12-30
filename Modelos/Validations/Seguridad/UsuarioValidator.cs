using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.Request.Usuarios;
using FluentValidation;


namespace Modelos.Validations.Seguridad
{
    public class UsuarioValidator : AbstractValidator<EntUsuarioRequest>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.sNombres)
                .NotEmpty().WithMessage("El campo Nombres es obligatorio.");

            RuleFor(x => x.sApellidos)
                .NotEmpty().WithMessage("El campo Apellidos es obligatorio.");

            RuleFor(x => x.sCorreo)
                .NotEmpty().WithMessage("El campo Correo es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electrónico es inválido.");

            RuleFor(x => x.sTelefono)
                .NotEmpty().WithMessage("El campo Teléfono es obligatorio.")
                .Matches(@"^\d{10}$").WithMessage("El número de teléfono debe tener 10 dígitos numéricos.");

            RuleFor(x => x.sContra)
                .NotEmpty().WithMessage("El campo 'Contraseña' es obligatorio.");
        }
    }
}
