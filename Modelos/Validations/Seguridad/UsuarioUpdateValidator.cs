using Modelos.Request.Usuarios;
using FluentValidation;

namespace Modelos.Validations.Seguridad
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
        }
    }
}
