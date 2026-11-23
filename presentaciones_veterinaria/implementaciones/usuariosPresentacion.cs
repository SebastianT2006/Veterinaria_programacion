using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class usuariosPresentacion : IusuariosPresentacion
{
    private readonly IComunicaciones comunicaciones;

    public usuariosPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }

    public List<usuarios> Consultar() => Ejecutar<List<usuarios>>("usuarios/Consultar", null);

    public List<usuarios> Buscar(usuarios e) => Ejecutar<List<usuarios>>("usuarios/Buscar", e);

    public usuarios Insertar(usuarios e) => Ejecutar<usuarios>("usuarios/Insertar", e);

    public usuarios Actualizar(usuarios e) => Ejecutar<usuarios>("usuarios/Actualizar", e);

    public usuarios Borrar(usuarios e) => Ejecutar<usuarios>("usuarios/Borrar", e);

    public sesionUsuario Login(credenciales e) => Ejecutar<sesionUsuario>("usuarios/Login", e);

    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
