using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class facturasPresentacion : IfacturasPresentacion
{
    private readonly IComunicaciones comunicaciones; public facturasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<facturas> Consultar() => Ejecutar<List<facturas>>("facturas/Consultar", null); public List<facturas> Buscar(facturas e) => Ejecutar<List<facturas>>("facturas/Buscar", e); public facturas Insertar(facturas e) => Ejecutar<facturas>("facturas/Insertar", e); public facturas Actualizar(facturas e) => Ejecutar<facturas>("facturas/Actualizar", e); public facturas Borrar(facturas e) => Ejecutar<facturas>("facturas/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
