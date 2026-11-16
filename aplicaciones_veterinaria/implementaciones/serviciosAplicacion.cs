using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class serviciosAplicacion : IserviciosAplicacion
{
    private readonly IConexion conexion;
    public serviciosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public servicios Actualizar(servicios entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<servicios>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public servicios Borrar(servicios entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.servicios!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<servicios> Buscar(servicios entidad)
    {
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Nombre)
            && string.IsNullOrWhiteSpace(entidad.Descripcion)
            && string.IsNullOrWhiteSpace(entidad.Categoria))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Nombre) && x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) || (!string.IsNullOrWhiteSpace(entidad.Descripcion) && x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion!)) || (!string.IsNullOrWhiteSpace(entidad.Categoria) && x.Categoria != null && x.Categoria.Contains(entidad.Categoria!))).Take(100).ToList();
    }
    public List<servicios> Consultar() => conexion.servicios!.Take(100).ToList();
    public servicios Insert(servicios entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.servicios!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(servicios entidad) => string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Descripcion) || string.IsNullOrEmpty(entidad.Categoria);
}
