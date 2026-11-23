using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class productosPresentacion : IproductosPresentacion
{
    private readonly IComunicaciones comunicaciones; public productosPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<productos> Consultar() => Ejecutar<List<productos>>("productos/Consultar", null); public List<productos> Buscar(productos e) => Ejecutar<List<productos>>("productos/Buscar", e); public productos Insertar(productos e) => Ejecutar<productos>("productos/Insertar", e); public productos Actualizar(productos e) => Ejecutar<productos>("productos/Actualizar", e); public productos Borrar(productos e) => Ejecutar<productos>("productos/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
