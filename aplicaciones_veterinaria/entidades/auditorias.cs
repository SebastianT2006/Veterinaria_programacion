namespace lib_aplicaciones.entidades
{
    public class auditorias
    {
        public int Id { get; set; }
        public int? Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string? Accion { get; set; }
        public string? Entidad { get; set; }
        public int? Registro { get; set; }
        public string? Detalle { get; set; }
    }
}
