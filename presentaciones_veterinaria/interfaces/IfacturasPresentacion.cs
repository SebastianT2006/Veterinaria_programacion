using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IfacturasPresentacion
{
    List<facturas> Consultar(); List<facturas> Buscar(facturas entidad); facturas Insertar(facturas entidad); facturas Actualizar(facturas entidad); facturas Borrar(facturas entidad);
}
