using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class razasAplicacion : IrazasAplicacion
{
    private readonly IConexion conexion;
    public razasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public razas Actualizar(razas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<razas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public razas Borrar(razas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.razas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<razas> Buscar(razas entidad)
    {
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Nombre)
            && string.IsNullOrWhiteSpace(entidad.Descripcion))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Nombre) && x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) || (!string.IsNullOrWhiteSpace(entidad.Descripcion) && x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion!))).Take(100).ToList();
    }
    public List<razas> Consultar() => conexion.razas!.Take(100).ToList();
    public razas Insert(razas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.razas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(razas entidad) => string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Descripcion);
}
