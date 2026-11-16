using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class personasAplicacion : IpersonasAplicacion
{
    private readonly IConexion conexion;
    public personasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public personas Actualizar(personas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<personas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public personas Borrar(personas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.personas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<personas> Buscar(personas entidad)
    {
        var lista = Consultar();

        if (string.IsNullOrWhiteSpace(entidad.Nombre)
            && string.IsNullOrWhiteSpace(entidad.Telefono)
            && string.IsNullOrWhiteSpace(entidad.Email)
            && string.IsNullOrWhiteSpace(entidad.Direccion))
            return lista;

        return lista.Where(x =>
               (!string.IsNullOrWhiteSpace(entidad.Nombre) && x.Nombre != null && x.Nombre.Contains(entidad.Nombre!))
            || (!string.IsNullOrWhiteSpace(entidad.Telefono) && x.Telefono != null && x.Telefono.Contains(entidad.Telefono!))
            || (!string.IsNullOrWhiteSpace(entidad.Email) && x.Email != null && x.Email.Contains(entidad.Email!))
            || (!string.IsNullOrWhiteSpace(entidad.Direccion) && x.Direccion != null && x.Direccion.Contains(entidad.Direccion!))
        ).Take(100).ToList();
    }
    public List<personas> Consultar() => conexion.personas!.Take(100).ToList();
    public personas Insert(personas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.personas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(personas entidad) => string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Telefono) || string.IsNullOrEmpty(entidad.Email) || string.IsNullOrEmpty(entidad.Direccion);
}
