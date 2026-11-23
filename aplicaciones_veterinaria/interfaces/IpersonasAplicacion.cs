using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IpersonasAplicacion
{
    personas Insert(personas entidad); List<personas> Consultar(); List<personas> Buscar(personas entidad); personas Actualizar(personas entidad); personas Borrar(personas entidad);
}
