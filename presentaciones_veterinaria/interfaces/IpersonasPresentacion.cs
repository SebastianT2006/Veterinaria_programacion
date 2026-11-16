using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IpersonasPresentacion
{
    List<personas> Consultar(); List<personas> Buscar(personas entidad); personas Insertar(personas entidad); personas Actualizar(personas entidad); personas Borrar(personas entidad);
}
