namespace lib_aplicaciones.entidades
{
    public class usuarios
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public int Rol { get; set; }
        public string? Estado { get; set; }
    }
}
