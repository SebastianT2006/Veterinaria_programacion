using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class vacunacionesPresentacion : IvacunacionesPresentacion
{
    private readonly IComunicaciones comunicaciones; public vacunacionesPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<vacunaciones> Consultar() => Ejecutar<List<vacunaciones>>("vacunaciones/Consultar", null); public List<vacunaciones> Buscar(vacunaciones e) => Ejecutar<List<vacunaciones>>("vacunaciones/Buscar", e); public vacunaciones Insertar(vacunaciones e) => Ejecutar<vacunaciones>("vacunaciones/Insertar", e); public vacunaciones Actualizar(vacunaciones e) => Ejecutar<vacunaciones>("vacunaciones/Actualizar", e); public vacunaciones Borrar(vacunaciones e) => Ejecutar<vacunaciones>("vacunaciones/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
