using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IrazasPresentacion
{
    List<razas> Consultar(); List<razas> Buscar(razas entidad); razas Insertar(razas entidad); razas Actualizar(razas entidad); razas Borrar(razas entidad);
}
