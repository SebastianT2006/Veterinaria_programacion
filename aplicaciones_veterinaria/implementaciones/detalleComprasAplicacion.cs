using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
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
        Validar(entidad);
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
        Validar(entidad);
        conexion.detalleCompras!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(detalleCompras entidad)
    {
        if (entidad.Cantidad <= 0)
            throw new ValidacionException("El campo cantidad debe ser mayor que cero");
        if (entidad.Precio < 0)
            throw new ValidacionException("El campo precio no puede ser negativo");
        if (!conexion.compras!.Any(x => x.Id == entidad.Compra))
            throw new ValidacionException("La compra seleccionada no existe");
        if (!conexion.productos!.Any(x => x.Id == entidad.Producto))
            throw new ValidacionException("El producto seleccionado no existe");
    }
}
