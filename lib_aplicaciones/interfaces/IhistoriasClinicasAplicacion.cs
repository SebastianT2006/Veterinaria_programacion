using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IhistoriasClinicasAplicacion
    {
        historiasClinicas Insert(historiasClinicas entidad);
        List<historiasClinicas> Consultar();
        historiasClinicas Actualizar(historiasClinicas entidad);
        historiasClinicas Borrar(historiasClinicas entidad);
    }
}