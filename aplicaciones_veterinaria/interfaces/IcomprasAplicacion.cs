using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IcomprasAplicacion
{
    compras Insert(compras entidad); List<compras> Consultar(); List<compras> Buscar(compras entidad); compras Actualizar(compras entidad); compras Borrar(compras entidad);
}
