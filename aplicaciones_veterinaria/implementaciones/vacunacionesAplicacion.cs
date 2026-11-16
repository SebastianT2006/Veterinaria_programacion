using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
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
        if (Validar(entidad))
            throw new Exception("Falta información");
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
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Observacion)
            && string.IsNullOrWhiteSpace(entidad.Lote))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Observacion) && x.Observacion != null && x.Observacion.Contains(entidad.Observacion!)) || (!string.IsNullOrWhiteSpace(entidad.Lote) && x.Lote != null && x.Lote.Contains(entidad.Lote!))).Take(100).ToList();
    }
    public List<vacunaciones> Consultar() => conexion.vacunaciones!.Take(100).ToList();
    public vacunaciones Insert(vacunaciones entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.vacunaciones!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(vacunaciones entidad) => string.IsNullOrEmpty(entidad.Observacion) || string.IsNullOrEmpty(entidad.Lote);
}
