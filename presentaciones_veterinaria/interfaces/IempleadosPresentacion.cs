using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IempleadosPresentacion
{
    List<empleados> Consultar(); List<empleados> Buscar(empleados entidad); empleados Insertar(empleados entidad); empleados Actualizar(empleados entidad); empleados Borrar(empleados entidad);
}
