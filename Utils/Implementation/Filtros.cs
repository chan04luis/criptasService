using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using Utils.Interfaces;


namespace Utils.Implementation
{
    public class Filtros : IFiltros
    {
        public bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{10}$");
        }

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public bool IsValidFecha(string fecha)
        {
            if (DateTime.TryParse(fecha, out DateTime fechaNac))
            {
                return true;
            }
            return false;
        }
        public bool IsValidSexo(string sexo)
        {
            if (sexo.ToLower()=="hombre" || sexo.ToLower() == "mujer")
            {
                return true;
            }
            return false;
        }

        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        public string? HashPassword(string? password)
        {
            if(password == null) return string.Empty;
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
