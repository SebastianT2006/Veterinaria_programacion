using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IproveedoresAplicacion
{
    proveedores Insert(proveedores entidad); List<proveedores> Consultar(); List<proveedores> Buscar(proveedores entidad); proveedores Actualizar(proveedores entidad); proveedores Borrar(proveedores entidad);
}
