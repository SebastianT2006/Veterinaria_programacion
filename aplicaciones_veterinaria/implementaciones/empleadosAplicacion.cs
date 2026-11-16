using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class empleadosAplicacion : IempleadosAplicacion
{
    private readonly IConexion conexion;
    public empleadosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public empleados Actualizar(empleados entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<empleados>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public empleados Borrar(empleados entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.empleados!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<empleados> Buscar(empleados entidad)
    {
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Cargo)
            && string.IsNullOrWhiteSpace(entidad.Estado)
            && string.IsNullOrWhiteSpace(entidad.Turno))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Cargo) && x.Cargo != null && x.Cargo.Contains(entidad.Cargo!)) || (!string.IsNullOrWhiteSpace(entidad.Estado) && x.Estado != null && x.Estado.Contains(entidad.Estado!)) || (!string.IsNullOrWhiteSpace(entidad.Turno) && x.Turno != null && x.Turno.Contains(entidad.Turno!))).Take(100).ToList();
    }
    public List<empleados> Consultar() => conexion.empleados!.Take(100).ToList();
    public empleados Insert(empleados entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.empleados!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(empleados entidad) => string.IsNullOrEmpty(entidad.Cargo) || string.IsNullOrEmpty(entidad.Estado) || string.IsNullOrEmpty(entidad.Turno);
}
