using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IfacturasAplicacion
    {
        facturas Insert(facturas entidad);
        List<facturas> Consultar();
        facturas Actualizar(facturas entidad);
        facturas Borrar(facturas entidad);
    }
}