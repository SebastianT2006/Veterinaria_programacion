using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class fincasPresentacion : IfincasPresentacion
{
    private readonly IComunicaciones comunicaciones; public fincasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<fincas> Consultar() => Ejecutar<List<fincas>>("fincas/Consultar", null); public List<fincas> Buscar(fincas e) => Ejecutar<List<fincas>>("fincas/Buscar", e); public fincas Insertar(fincas e) => Ejecutar<fincas>("fincas/Insertar", e); public fincas Actualizar(fincas e) => Ejecutar<fincas>("fincas/Actualizar", e); public fincas Borrar(fincas e) => Ejecutar<fincas>("fincas/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
