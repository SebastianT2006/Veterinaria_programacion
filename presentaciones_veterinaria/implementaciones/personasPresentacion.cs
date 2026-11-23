using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class personasPresentacion : IpersonasPresentacion
{
    private readonly IComunicaciones comunicaciones; public personasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<personas> Consultar() => Ejecutar<List<personas>>("personas/Consultar", null); public List<personas> Buscar(personas e) => Ejecutar<List<personas>>("personas/Buscar", e); public personas Insertar(personas e) => Ejecutar<personas>("personas/Insertar", e); public personas Actualizar(personas e) => Ejecutar<personas>("personas/Actualizar", e); public personas Borrar(personas e) => Ejecutar<personas>("personas/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
