namespace lib_aplicaciones.entidades
{
    public class detalleFacturas
    {
        public int Id { get; set; }
        public int Factura { get; set; }
        public int? Producto { get; set; }
        public int? Servicio { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}