using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class empleadosPresentacion : IempleadosPresentacion
{
    private readonly IComunicaciones comunicaciones; public empleadosPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<empleados> Consultar() => Ejecutar<List<empleados>>("empleados/Consultar", null); public List<empleados> Buscar(empleados e) => Ejecutar<List<empleados>>("empleados/Buscar", e); public empleados Insertar(empleados e) => Ejecutar<empleados>("empleados/Insertar", e); public empleados Actualizar(empleados e) => Ejecutar<empleados>("empleados/Actualizar", e); public empleados Borrar(empleados e) => Ejecutar<empleados>("empleados/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
