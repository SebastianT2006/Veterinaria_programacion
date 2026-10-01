using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IdetalleFacturasAplicacion
    {
        detalleFacturas Insert(detalleFacturas entidad);
        List<detalleFacturas> Consultar();
        detalleFacturas Actualizar(detalleFacturas entidad);
        detalleFacturas Borrar(detalleFacturas entidad);
    }
}