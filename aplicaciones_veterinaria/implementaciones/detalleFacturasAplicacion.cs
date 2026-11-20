using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
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
        Validar(entidad);
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
        Validar(entidad);
        conexion.detalleFacturas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(detalleFacturas entidad)
    {
        if (entidad.Cantidad <= 0)
            throw new ValidacionException("El campo cantidad debe ser mayor que cero");
        if (entidad.Precio < 0)
            throw new ValidacionException("El campo precio no puede ser negativo");
        if (entidad.Producto == null && entidad.Servicio == null)
            throw new ValidacionException("Debe indicar un producto o un servicio");
        if (!conexion.facturas!.Any(x => x.Id == entidad.Factura))
            throw new ValidacionException("La factura seleccionada no existe");
        if (entidad.Producto != null && !conexion.productos!.Any(x => x.Id == entidad.Producto))
            throw new ValidacionException("El producto seleccionado no existe");
        if (entidad.Servicio != null && !conexion.servicios!.Any(x => x.Id == entidad.Servicio))
            throw new ValidacionException("El servicio seleccionado no existe");
    }
}
