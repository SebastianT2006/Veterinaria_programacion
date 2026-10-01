namespace lib_aplicaciones.entidades
{
    public class citas
    {
        public int Id { get; set; }
        public int Animal { get; set; }
        public int Veterinario { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string? Motivo { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
    }
}