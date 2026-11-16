using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class clientesPresentacion : IclientesPresentacion
{
    private readonly IComunicaciones comunicaciones; public clientesPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<clientes> Consultar() => Ejecutar<List<clientes>>("clientes/Consultar", null); public List<clientes> Buscar(clientes e) => Ejecutar<List<clientes>>("clientes/Buscar", e); public clientes Insertar(clientes e) => Ejecutar<clientes>("clientes/Insertar", e); public clientes Actualizar(clientes e) => Ejecutar<clientes>("clientes/Actualizar", e); public clientes Borrar(clientes e) => Ejecutar<clientes>("clientes/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
