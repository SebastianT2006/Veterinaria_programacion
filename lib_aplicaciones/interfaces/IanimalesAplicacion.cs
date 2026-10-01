using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IanimalesAplicacion
    {
        animales Insert(animales entidad);
        List<animales> Consultar();
        animales Actualizar(animales entidad);
        animales Borrar(animales entidad);
    }
}