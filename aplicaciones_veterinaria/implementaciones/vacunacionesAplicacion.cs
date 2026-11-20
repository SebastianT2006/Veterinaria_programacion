using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class vacunacionesAplicacion : IvacunacionesAplicacion
{
    private readonly IConexion conexion;
    public vacunacionesAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public vacunaciones Actualizar(vacunaciones entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<vacunaciones>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public vacunaciones Borrar(vacunaciones entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.vacunaciones!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<vacunaciones> Buscar(vacunaciones entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Observacion: se usa como palabra clave
        var palabra = entidad.Observacion;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.vacunaciones!
            .Where(x => x.Observacion!.Contains(palabra) || x.Lote!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<vacunaciones> Consultar() => conexion.vacunaciones!.Take(100).ToList();
    public vacunaciones Insert(vacunaciones entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.vacunaciones!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(vacunaciones entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Lote))
            throw new ValidacionException("El campo lote es obligatorio");
        if (entidad.Fecha < new DateTime(1900, 1, 1) || entidad.Fecha > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha no es válida");
        if (entidad.ProximaFecha < new DateTime(1900, 1, 1) || entidad.ProximaFecha > new DateTime(2079, 6, 6))
            throw new ValidacionException("La próxima fecha no es válida");
        if (entidad.Dosis <= 0)
            throw new ValidacionException("El campo dosis debe ser mayor que cero");
        if (!conexion.animales!.Any(x => x.Id == entidad.Animal))
            throw new ValidacionException("El animal seleccionado no existe");
        if (!conexion.productos!.Any(x => x.Id == entidad.Producto))
            throw new ValidacionException("El producto seleccionado no existe");
    }
}
