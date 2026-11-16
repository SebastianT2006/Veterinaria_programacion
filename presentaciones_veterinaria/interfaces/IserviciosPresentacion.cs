using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IserviciosPresentacion
{
    List<servicios> Consultar(); List<servicios> Buscar(servicios entidad); servicios Insertar(servicios entidad); servicios Actualizar(servicios entidad); servicios Borrar(servicios entidad);
}
