using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IrolesAplicacion
{
    roles Insert(roles entidad);
    List<roles> Consultar();
    List<roles> Buscar(roles entidad);
    roles Actualizar(roles entidad);
    roles Borrar(roles entidad);
}
