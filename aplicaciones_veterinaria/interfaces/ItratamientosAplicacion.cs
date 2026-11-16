using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface ItratamientosAplicacion
{
    tratamientos Insert(tratamientos entidad); List<tratamientos> Consultar(); List<tratamientos> Buscar(tratamientos entidad); tratamientos Actualizar(tratamientos entidad); tratamientos Borrar(tratamientos entidad);
}
