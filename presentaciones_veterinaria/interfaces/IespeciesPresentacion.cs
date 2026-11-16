using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IespeciesPresentacion
{
    List<especies> Consultar(); List<especies> Buscar(especies entidad); especies Insertar(especies entidad); especies Actualizar(especies entidad); especies Borrar(especies entidad);
}
