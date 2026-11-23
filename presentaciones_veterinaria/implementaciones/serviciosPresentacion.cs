using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class serviciosPresentacion : IserviciosPresentacion
{
    private readonly IComunicaciones comunicaciones; public serviciosPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<servicios> Consultar() => Ejecutar<List<servicios>>("servicios/Consultar", null); public List<servicios> Buscar(servicios e) => Ejecutar<List<servicios>>("servicios/Buscar", e); public servicios Insertar(servicios e) => Ejecutar<servicios>("servicios/Insertar", e); public servicios Actualizar(servicios e) => Ejecutar<servicios>("servicios/Actualizar", e); public servicios Borrar(servicios e) => Ejecutar<servicios>("servicios/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
