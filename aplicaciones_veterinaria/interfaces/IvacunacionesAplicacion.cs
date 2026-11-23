using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IvacunacionesAplicacion
{
    vacunaciones Insert(vacunaciones entidad); List<vacunaciones> Consultar(); List<vacunaciones> Buscar(vacunaciones entidad); vacunaciones Actualizar(vacunaciones entidad); vacunaciones Borrar(vacunaciones entidad);
}
