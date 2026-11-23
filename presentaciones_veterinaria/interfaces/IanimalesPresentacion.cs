using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IanimalesPresentacion
{
    List<animales> Consultar(); List<animales> Buscar(animales entidad); animales Insertar(animales entidad); animales Actualizar(animales entidad); animales Borrar(animales entidad);
}
