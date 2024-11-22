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
    }
}
