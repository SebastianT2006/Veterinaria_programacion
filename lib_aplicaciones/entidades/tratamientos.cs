namespace lib_aplicaciones.entidades
{
    public class tratamientos
    {
        public int Id { get; set; }
        public int Animal { get; set; }
        public int Consulta { get; set; }
        public int Producto { get; set; }
        public decimal Dosis { get; set; }
        public string? Frecuencia { get; set; }
        public int Duracion { get; set; }
        public string? Indicaciones { get; set; }
        public DateTime FechaInicio { get; set; }
    }
}