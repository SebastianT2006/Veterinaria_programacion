namespace lib_aplicaciones.entidades
{
    public class vacunaciones
    {
        public int Id { get; set; }
        public int Animal { get; set; }
        public int Producto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Observacion { get; set; }
        public DateTime ProximaFecha { get; set; }
        public string? Lote { get; set; }
        public decimal Dosis { get; set; }
    }
}