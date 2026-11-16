using lib_aplicaciones.entidades;
using presentaciones_veterinaria.interfaces;
namespace presentaciones_veterinaria.implementaciones;

public class historiasClinicasPresentacion : IhistoriasClinicasPresentacion
{
    private readonly IComunicaciones comunicaciones; public historiasClinicasPresentacion(IComunicaciones comunicaciones)
    {
        this.comunicaciones = comunicaciones;
    }
    public List<historiasClinicas> Consultar() => Ejecutar<List<historiasClinicas>>("historiasClinicas/Consultar", null); public List<historiasClinicas> Buscar(historiasClinicas e) => Ejecutar<List<historiasClinicas>>("historiasClinicas/Buscar", e); public historiasClinicas Insertar(historiasClinicas e) => Ejecutar<historiasClinicas>("historiasClinicas/Insertar", e); public historiasClinicas Actualizar(historiasClinicas e) => Ejecutar<historiasClinicas>("historiasClinicas/Actualizar", e); public historiasClinicas Borrar(historiasClinicas e) => Ejecutar<historiasClinicas>("historiasClinicas/Borrar", e);
    private T Ejecutar<T>(string ruta, object? e)
    {
        var tarea = comunicaciones.Ejecutar<T>(ruta, e);
        tarea.Wait();
        return tarea.Result;
    }
}
