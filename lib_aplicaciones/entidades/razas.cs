namespace lib_aplicaciones.entidades
{
    public class razas
    {
        public int Id { get; set; }
        public int Especie { get; set; }
        public string? Nombre { get; set; }
        public decimal TamanoPromedio { get; set; }
        public decimal PesoPromedio { get; set; }
        public int EsperanzaVidaAnios { get; set; }
        public string? Descripcion { get; set; }
    }
}