namespace lib_aplicaciones.entidades
{
    public class consultas
    {
        public int Id { get; set; }
        public int Cita { get; set; }
        public int Animal { get; set; }
        public int Veterinario { get; set; }
        public DateTime Fecha { get; set; }
        public string? Diagnostico { get; set; }
        public string? Observaciones { get; set; }
        public decimal PesoActual { get; set; }
        public decimal Temperatura { get; set; }
    }
}