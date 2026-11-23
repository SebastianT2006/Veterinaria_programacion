using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class tratamientosAplicacion : ItratamientosAplicacion
{
    private readonly IConexion conexion;
    public tratamientosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public tratamientos Actualizar(tratamientos entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<tratamientos>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public tratamientos Borrar(tratamientos entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.tratamientos!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<tratamientos> Buscar(tratamientos entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Frecuencia: se usa como palabra clave
        var palabra = entidad.Frecuencia;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.tratamientos!
            .Where(x => x.Frecuencia!.Contains(palabra) || x.Indicaciones!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<tratamientos> Consultar() => conexion.tratamientos!.Take(100).ToList();
    public tratamientos Insert(tratamientos entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.tratamientos!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(tratamientos entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Frecuencia))
            throw new ValidacionException("El campo frecuencia es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Indicaciones))
            throw new ValidacionException("El campo indicaciones es obligatorio");
        if (entidad.FechaInicio < new DateTime(1900, 1, 1) || entidad.FechaInicio > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha de inicio no es válida");
        if (entidad.Dosis <= 0)
            throw new ValidacionException("El campo dosis debe ser mayor que cero");
        if (entidad.Duracion <= 0)
            throw new ValidacionException("El campo duración debe ser mayor que cero");
        if (!conexion.animales!.Any(x => x.Id == entidad.Animal))
            throw new ValidacionException("El animal seleccionado no existe");
        if (!conexion.consultas!.Any(x => x.Id == entidad.Consulta))
            throw new ValidacionException("La consulta seleccionada no existe");
        if (!conexion.productos!.Any(x => x.Id == entidad.Producto))
            throw new ValidacionException("El producto seleccionado no existe");
    }
}
