using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class rolesPresentacion : IrolesPresentacion
{
    private readonly IComunicaciones comunicaciones;

    public rolesPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }

    public List<roles> Consultar() => Ejecutar<List<roles>>("roles/Consultar", null);

    public List<roles> Buscar(roles e) => Ejecutar<List<roles>>("roles/Buscar", e);

    public roles Insertar(roles e) => Ejecutar<roles>("roles/Insertar", e);

    public roles Actualizar(roles e) => Ejecutar<roles>("roles/Actualizar", e);

    public roles Borrar(roles e) => Ejecutar<roles>("roles/Borrar", e);

    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
