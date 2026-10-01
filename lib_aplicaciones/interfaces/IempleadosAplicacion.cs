using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IempleadosAplicacion
    {
        empleados Insert(empleados entidad);
        List<empleados> Consultar();
        empleados Actualizar(empleados entidad);
        empleados Borrar(empleados entidad);
    }
}