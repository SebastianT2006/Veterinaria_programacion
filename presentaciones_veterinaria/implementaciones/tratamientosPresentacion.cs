using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class tratamientosPresentacion : ItratamientosPresentacion
{
    private readonly IComunicaciones comunicaciones; public tratamientosPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<tratamientos> Consultar() => Ejecutar<List<tratamientos>>("tratamientos/Consultar", null); public List<tratamientos> Buscar(tratamientos e) => Ejecutar<List<tratamientos>>("tratamientos/Buscar", e); public tratamientos Insertar(tratamientos e) => Ejecutar<tratamientos>("tratamientos/Insertar", e); public tratamientos Actualizar(tratamientos e) => Ejecutar<tratamientos>("tratamientos/Actualizar", e); public tratamientos Borrar(tratamientos e) => Ejecutar<tratamientos>("tratamientos/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
