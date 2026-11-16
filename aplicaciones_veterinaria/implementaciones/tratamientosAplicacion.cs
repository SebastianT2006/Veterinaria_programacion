using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
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
        if (Validar(entidad))
            throw new Exception("Falta información");
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
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Frecuencia)
            && string.IsNullOrWhiteSpace(entidad.Indicaciones))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Frecuencia) && x.Frecuencia != null && x.Frecuencia.Contains(entidad.Frecuencia!)) || (!string.IsNullOrWhiteSpace(entidad.Indicaciones) && x.Indicaciones != null && x.Indicaciones.Contains(entidad.Indicaciones!))).Take(100).ToList();
    }
    public List<tratamientos> Consultar() => conexion.tratamientos!.Take(100).ToList();
    public tratamientos Insert(tratamientos entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.tratamientos!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(tratamientos entidad) => string.IsNullOrEmpty(entidad.Frecuencia) || string.IsNullOrEmpty(entidad.Indicaciones);
}
