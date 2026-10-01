using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IrazasAplicacion
    {
        razas Insert(razas entidad);
        List<razas> Consultar();
        razas Actualizar(razas entidad);
        razas Borrar(razas entidad);
    }
}