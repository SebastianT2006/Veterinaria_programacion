namespace lib_aplicaciones.entidades
{
    public class facturas
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string? MetodoPago { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
    }
}