using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface ItratamientosAplicacion
    {
        tratamientos Insert(tratamientos entidad);
        List<tratamientos> Consultar();
        tratamientos Actualizar(tratamientos entidad);
        tratamientos Borrar(tratamientos entidad);
    }
}