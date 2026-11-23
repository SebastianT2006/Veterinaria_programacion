using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class veterinariosPresentacion : IveterinariosPresentacion
{
    private readonly IComunicaciones comunicaciones; public veterinariosPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<veterinarios> Consultar() => Ejecutar<List<veterinarios>>("veterinarios/Consultar", null); public List<veterinarios> Buscar(veterinarios e) => Ejecutar<List<veterinarios>>("veterinarios/Buscar", e); public veterinarios Insertar(veterinarios e) => Ejecutar<veterinarios>("veterinarios/Insertar", e); public veterinarios Actualizar(veterinarios e) => Ejecutar<veterinarios>("veterinarios/Actualizar", e); public veterinarios Borrar(veterinarios e) => Ejecutar<veterinarios>("veterinarios/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
