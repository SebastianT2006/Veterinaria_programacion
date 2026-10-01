using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IclientesAplicacion
    {
        clientes Insert(clientes entidad);
        List<clientes> Consultar();
        clientes Actualizar(clientes entidad);
        clientes Borrar(clientes entidad);
    }
}