using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class comprasPresentacion : IcomprasPresentacion
{
    private readonly IComunicaciones comunicaciones; public comprasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<compras> Consultar() => Ejecutar<List<compras>>("compras/Consultar", null); public List<compras> Buscar(compras e) => Ejecutar<List<compras>>("compras/Buscar", e); public compras Insertar(compras e) => Ejecutar<compras>("compras/Insertar", e); public compras Actualizar(compras e) => Ejecutar<compras>("compras/Actualizar", e); public compras Borrar(compras e) => Ejecutar<compras>("compras/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
