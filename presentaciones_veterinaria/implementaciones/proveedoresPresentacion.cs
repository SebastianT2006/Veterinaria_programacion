using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class proveedoresPresentacion : IproveedoresPresentacion
{
    private readonly IComunicaciones comunicaciones; public proveedoresPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<proveedores> Consultar() => Ejecutar<List<proveedores>>("proveedores/Consultar", null); public List<proveedores> Buscar(proveedores e) => Ejecutar<List<proveedores>>("proveedores/Buscar", e); public proveedores Insertar(proveedores e) => Ejecutar<proveedores>("proveedores/Insertar", e); public proveedores Actualizar(proveedores e) => Ejecutar<proveedores>("proveedores/Actualizar", e); public proveedores Borrar(proveedores e) => Ejecutar<proveedores>("proveedores/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
