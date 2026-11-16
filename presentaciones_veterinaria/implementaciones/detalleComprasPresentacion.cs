using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class detalleComprasPresentacion : IdetalleComprasPresentacion
{
    private readonly IComunicaciones comunicaciones; public detalleComprasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<detalleCompras> Consultar() => Ejecutar<List<detalleCompras>>("detalleCompras/Consultar", null); public List<detalleCompras> Buscar(detalleCompras e) => Ejecutar<List<detalleCompras>>("detalleCompras/Buscar", e); public detalleCompras Insertar(detalleCompras e) => Ejecutar<detalleCompras>("detalleCompras/Insertar", e); public detalleCompras Actualizar(detalleCompras e) => Ejecutar<detalleCompras>("detalleCompras/Actualizar", e); public detalleCompras Borrar(detalleCompras e) => Ejecutar<detalleCompras>("detalleCompras/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
