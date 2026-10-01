namespace lib_aplicaciones.entidades
{
    public class clientes
    {
        public int Id { get; set; }
        public int Persona { get; set; }
        public string? TipoCliente { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public string? PreferenciaContacto { get; set; }
    }
}