using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IproveedoresAplicacion
    {
        proveedores Insert(proveedores entidad);
        List<proveedores> Consultar();
        proveedores Actualizar(proveedores entidad);
        proveedores Borrar(proveedores entidad);
    }
}