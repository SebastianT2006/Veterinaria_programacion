using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class detalleComprasAplicacion : IdetalleComprasAplicacion
{
    private readonly IConexion conexion;
    public detalleComprasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public detalleCompras Actualizar(detalleCompras entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<detalleCompras>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public detalleCompras Borrar(detalleCompras entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.detalleCompras!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<detalleCompras> Buscar(detalleCompras entidad)
    {
        var lista = Consultar();
        return lista.Where(x => true).Take(100).ToList();
    }
    public List<detalleCompras> Consultar() => conexion.detalleCompras!.Take(100).ToList();
    public detalleCompras Insert(detalleCompras entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.detalleCompras!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(detalleCompras entidad) => false;
}
