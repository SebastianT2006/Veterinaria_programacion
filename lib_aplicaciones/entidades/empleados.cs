namespace lib_aplicaciones.entidades
{
    public class empleados
    {
        public int Id { get; set; }
        public int Persona { get; set; }
        public string? Cargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal Salario { get; set; }
        public string? Estado { get; set; }
        public string? Turno { get; set; }
    }
}