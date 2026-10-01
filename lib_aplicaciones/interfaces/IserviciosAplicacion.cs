using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IserviciosAplicacion
    {
        servicios Insert(servicios entidad);
        List<servicios> Consultar();
        servicios Actualizar(servicios entidad);
        servicios Borrar(servicios entidad);
    }
}