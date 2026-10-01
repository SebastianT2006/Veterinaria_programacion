namespace lib_aplicaciones.entidades
{
    public class veterinarios
    {
        public int Id { get; set; }
        public int Persona { get; set; }
        public string? Especialidad { get; set; }
        public string? RegistroProfesional { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string? Estado { get; set; }
        public string? TelefonoLaboral { get; set; }
    }
}