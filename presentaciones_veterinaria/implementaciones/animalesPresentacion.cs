using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class animalesPresentacion : IanimalesPresentacion
{
    private readonly IComunicaciones comunicaciones; public animalesPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<animales> Consultar() => Ejecutar<List<animales>>("animales/Consultar", null); public List<animales> Buscar(animales e) => Ejecutar<List<animales>>("animales/Buscar", e); public animales Insertar(animales e) => Ejecutar<animales>("animales/Insertar", e); public animales Actualizar(animales e) => Ejecutar<animales>("animales/Actualizar", e); public animales Borrar(animales e) => Ejecutar<animales>("animales/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
