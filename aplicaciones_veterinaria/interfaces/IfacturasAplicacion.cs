using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IfacturasAplicacion
{
    facturas Insert(facturas entidad); List<facturas> Consultar(); List<facturas> Buscar(facturas entidad); facturas Actualizar(facturas entidad); facturas Borrar(facturas entidad);
}
