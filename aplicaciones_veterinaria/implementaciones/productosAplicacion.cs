using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class productosAplicacion : IproductosAplicacion
{
    private readonly IConexion conexion;
    public productosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public productos Actualizar(productos entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<productos>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public productos Borrar(productos entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.productos!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<productos> Buscar(productos entidad)
    {
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Nombre)
            && string.IsNullOrWhiteSpace(entidad.Tipo)
            && string.IsNullOrWhiteSpace(entidad.Descripcion))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Nombre) && x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) || (!string.IsNullOrWhiteSpace(entidad.Tipo) && x.Tipo != null && x.Tipo.Contains(entidad.Tipo!)) || (!string.IsNullOrWhiteSpace(entidad.Descripcion) && x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion!))).Take(100).ToList();
    }
    public List<productos> Consultar() => conexion.productos!.Take(100).ToList();
    public productos Insert(productos entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.productos!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(productos entidad) => string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Tipo) || string.IsNullOrEmpty(entidad.Descripcion);
}
