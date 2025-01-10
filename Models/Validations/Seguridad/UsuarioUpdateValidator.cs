using FluentValidation;
using Models.Request.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Validations.Seguridad
{
    public class UsuarioUpdateValidator : AbstractValidator<EntUsuarioUpdateRequest>
    {
        public UsuarioUpdateValidator()
        {
            RuleFor(x => x.sNombres)
                .NotEmpty().WithMessage("El campo Nombres es obligatorio.");

            RuleFor(x => x.sApellidos)
                .NotEmpty().WithMessage("El campo Apellidos es obligatorio.");

            RuleFor(x => x.sTelefono)
                .NotEmpty().WithMessage("El campo Teléfono es obligatorio.")
                .Matches(@"^\d{10}$").WithMessage("El número de teléfono debe tener 10 dígitos numéricos.");

            RuleFor(x => x.uIdPerfil)
               .NotEmpty().WithMessage("El campo IdPerfil es obligatorio.");
        }
    }
}
