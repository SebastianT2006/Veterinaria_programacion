using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class especiesPresentacion : IespeciesPresentacion
{
    private readonly IComunicaciones comunicaciones; public especiesPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<especies> Consultar() => Ejecutar<List<especies>>("especies/Consultar", null); public List<especies> Buscar(especies e) => Ejecutar<List<especies>>("especies/Buscar", e); public especies Insertar(especies e) => Ejecutar<especies>("especies/Insertar", e); public especies Actualizar(especies e) => Ejecutar<especies>("especies/Actualizar", e); public especies Borrar(especies e) => Ejecutar<especies>("especies/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
