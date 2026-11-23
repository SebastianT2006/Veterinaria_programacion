using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class auditoriasPresentacion : IauditoriasPresentacion
{
    private readonly IComunicaciones comunicaciones;

    public auditoriasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }

    public List<auditorias> Consultar() => Ejecutar<List<auditorias>>("auditorias/Consultar", null);

    public List<auditorias> Buscar(auditorias e) => Ejecutar<List<auditorias>>("auditorias/Buscar", e);

    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
