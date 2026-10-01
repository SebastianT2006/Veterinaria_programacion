namespace lib_aplicaciones.entidades
{
    public class compras
    {
        public int Id { get; set; }
        public int Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string? NumeroFactura { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
    }
}