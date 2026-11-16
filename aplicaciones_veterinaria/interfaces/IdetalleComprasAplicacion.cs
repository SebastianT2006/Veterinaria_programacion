using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IdetalleComprasAplicacion
{
    detalleCompras Insert(detalleCompras entidad); List<detalleCompras> Consultar(); List<detalleCompras> Buscar(detalleCompras entidad); detalleCompras Actualizar(detalleCompras entidad); detalleCompras Borrar(detalleCompras entidad);
}
