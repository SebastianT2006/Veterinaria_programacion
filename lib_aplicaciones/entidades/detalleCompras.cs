namespace lib_aplicaciones.entidades
{
    public class detalleCompras
    {
        public int Id { get; set; }
        public int Compra { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}