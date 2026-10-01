using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IcitasAplicacion
    {
        citas Insert(citas entidad);
        List<citas> Consultar();
        citas Actualizar(citas entidad);
        citas Borrar(citas entidad);
    }
}