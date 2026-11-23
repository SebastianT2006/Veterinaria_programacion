using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class consultasPresentacion : IconsultasPresentacion
{
    private readonly IComunicaciones comunicaciones; public consultasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<consultas> Consultar() => Ejecutar<List<consultas>>("consultas/Consultar", null); public List<consultas> Buscar(consultas e) => Ejecutar<List<consultas>>("consultas/Buscar", e); public consultas Insertar(consultas e) => Ejecutar<consultas>("consultas/Insertar", e); public consultas Actualizar(consultas e) => Ejecutar<consultas>("consultas/Actualizar", e); public consultas Borrar(consultas e) => Ejecutar<consultas>("consultas/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
