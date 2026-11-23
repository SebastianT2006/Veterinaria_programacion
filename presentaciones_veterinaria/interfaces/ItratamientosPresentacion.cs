using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface ItratamientosPresentacion
{
    List<tratamientos> Consultar(); List<tratamientos> Buscar(tratamientos entidad); tratamientos Insertar(tratamientos entidad); tratamientos Actualizar(tratamientos entidad); tratamientos Borrar(tratamientos entidad);
}
