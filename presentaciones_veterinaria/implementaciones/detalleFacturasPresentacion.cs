using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class detalleFacturasPresentacion : IdetalleFacturasPresentacion
{
    private readonly IComunicaciones comunicaciones; public detalleFacturasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<detalleFacturas> Consultar() => Ejecutar<List<detalleFacturas>>("detalleFacturas/Consultar", null); public List<detalleFacturas> Buscar(detalleFacturas e) => Ejecutar<List<detalleFacturas>>("detalleFacturas/Buscar", e); public detalleFacturas Insertar(detalleFacturas e) => Ejecutar<detalleFacturas>("detalleFacturas/Insertar", e); public detalleFacturas Actualizar(detalleFacturas e) => Ejecutar<detalleFacturas>("detalleFacturas/Actualizar", e); public detalleFacturas Borrar(detalleFacturas e) => Ejecutar<detalleFacturas>("detalleFacturas/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
