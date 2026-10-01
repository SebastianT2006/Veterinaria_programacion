namespace lib_aplicaciones.entidades
{
    public class animales
    {
        public int Id { get; set; }
        public int Raza { get; set; }
        public int Cliente { get; set; }
        public int Finca { get; set; }
        public string? Nombre { get; set; }
        public string? Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Color { get; set; }
        public decimal Peso { get; set; }
        public string? Estado { get; set; }
    }
}