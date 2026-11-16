using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IdetalleComprasPresentacion
{
    List<detalleCompras> Consultar(); List<detalleCompras> Buscar(detalleCompras entidad); detalleCompras Insertar(detalleCompras entidad); detalleCompras Actualizar(detalleCompras entidad); detalleCompras Borrar(detalleCompras entidad);
}
