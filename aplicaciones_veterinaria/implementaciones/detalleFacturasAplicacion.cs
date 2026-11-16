using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class detalleFacturasAplicacion : IdetalleFacturasAplicacion
{
    private readonly IConexion conexion;
    public detalleFacturasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public detalleFacturas Actualizar(detalleFacturas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<detalleFacturas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public detalleFacturas Borrar(detalleFacturas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.detalleFacturas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<detalleFacturas> Buscar(detalleFacturas entidad)
    {
        var lista = Consultar();
        return lista.Where(x => true).Take(100).ToList();
    }
    public List<detalleFacturas> Consultar() => conexion.detalleFacturas!.Take(100).ToList();
    public detalleFacturas Insert(detalleFacturas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.detalleFacturas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(detalleFacturas entidad) => false;
}
