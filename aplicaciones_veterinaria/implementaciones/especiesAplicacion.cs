using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class especiesAplicacion : IespeciesAplicacion
{
    private readonly IConexion conexion;
    public especiesAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public especies Actualizar(especies entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<especies>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public especies Borrar(especies entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.especies!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<especies> Buscar(especies entidad)
    {
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Nombre)
            && string.IsNullOrWhiteSpace(entidad.Descripcion))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Nombre) && x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) || (!string.IsNullOrWhiteSpace(entidad.Descripcion) && x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion!))).Take(100).ToList();
    }
    public List<especies> Consultar() => conexion.especies!.Take(100).ToList();
    public especies Insert(especies entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.especies!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(especies entidad) => string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Descripcion);
}
