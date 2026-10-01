using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IfincasAplicacion
    {
        fincas Insert(fincas entidad);
        List<fincas> Consultar();
        fincas Actualizar(fincas entidad);
        fincas Borrar(fincas entidad);
    }
}