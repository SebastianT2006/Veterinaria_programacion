using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IclientesAplicacion
{
    clientes Insert(clientes entidad); List<clientes> Consultar(); List<clientes> Buscar(clientes entidad); clientes Actualizar(clientes entidad); clientes Borrar(clientes entidad);
}
