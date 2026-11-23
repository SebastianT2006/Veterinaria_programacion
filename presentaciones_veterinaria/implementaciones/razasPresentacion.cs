using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class razasPresentacion : IrazasPresentacion
{
    private readonly IComunicaciones comunicaciones; public razasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<razas> Consultar() => Ejecutar<List<razas>>("razas/Consultar", null); public List<razas> Buscar(razas e) => Ejecutar<List<razas>>("razas/Buscar", e); public razas Insertar(razas e) => Ejecutar<razas>("razas/Insertar", e); public razas Actualizar(razas e) => Ejecutar<razas>("razas/Actualizar", e); public razas Borrar(razas e) => Ejecutar<razas>("razas/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
