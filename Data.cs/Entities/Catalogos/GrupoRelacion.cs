namespace Data.cs.Entities.Catalogos
{
    public class GrupoRelacion
    {
        public Guid Id { get; set; }

        public int IdDia { get; set; }

        public Guid IdAula { get; set; }

        public Guid IdHorario { get; set; }

        public Guid IdGrupo { get; set; }

        public Guid IdUsuario { get; set; }
    }
}
