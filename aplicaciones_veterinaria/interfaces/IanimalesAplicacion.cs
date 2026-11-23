using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IanimalesAplicacion
{
    animales Insert(animales entidad); List<animales> Consultar(); List<animales> Buscar(animales entidad); animales Actualizar(animales entidad); animales Borrar(animales entidad);
}
