namespace lib_aplicaciones.entidades
{
    public class fincas
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public decimal AreaHectareas { get; set; }
        public string? TipoProduccion { get; set; }
        public string? Estado { get; set; }
    }
}