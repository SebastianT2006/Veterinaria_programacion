using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IdetalleFacturasPresentacion
{
    List<detalleFacturas> Consultar(); List<detalleFacturas> Buscar(detalleFacturas entidad); detalleFacturas Insertar(detalleFacturas entidad); detalleFacturas Actualizar(detalleFacturas entidad); detalleFacturas Borrar(detalleFacturas entidad);
}
