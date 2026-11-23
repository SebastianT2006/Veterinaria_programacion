using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IproveedoresPresentacion
{
    List<proveedores> Consultar(); List<proveedores> Buscar(proveedores entidad); proveedores Insertar(proveedores entidad); proveedores Actualizar(proveedores entidad); proveedores Borrar(proveedores entidad);
}
