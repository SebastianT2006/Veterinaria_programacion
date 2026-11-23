using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IvacunacionesPresentacion
{
    List<vacunaciones> Consultar(); List<vacunaciones> Buscar(vacunaciones entidad); vacunaciones Insertar(vacunaciones entidad); vacunaciones Actualizar(vacunaciones entidad); vacunaciones Borrar(vacunaciones entidad);
}
