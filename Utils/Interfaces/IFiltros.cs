namespace Utils.Interfaces
{
    public interface IFiltros
    {
        bool IsValidPhone(string phone);
        bool IsValidEmail(string email);
        bool IsValidFecha(string fecha);
        bool IsValidSexo(string sexo);
    }
}
