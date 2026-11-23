using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class citasPresentacion : IcitasPresentacion
{
    private readonly IComunicaciones comunicaciones; public citasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<citas> Consultar() => Ejecutar<List<citas>>("citas/Consultar", null); public List<citas> Buscar(citas e) => Ejecutar<List<citas>>("citas/Buscar", e); public citas Insertar(citas e) => Ejecutar<citas>("citas/Insertar", e); public citas Actualizar(citas e) => Ejecutar<citas>("citas/Actualizar", e); public citas Borrar(citas e) => Ejecutar<citas>("citas/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
