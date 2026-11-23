using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class comprasAplicacion : IcomprasAplicacion
{
    private readonly IConexion conexion;
    public comprasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public compras Actualizar(compras entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<compras>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public compras Borrar(compras entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.compras!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<compras> Buscar(compras entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en NumeroFactura: se usa como palabra clave
        var palabra = entidad.NumeroFactura;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.compras!
            .Where(x => x.NumeroFactura!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<compras> Consultar() => conexion.compras!.Take(100).ToList();
    public compras Insert(compras entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.compras!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(compras entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.NumeroFactura))
            throw new ValidacionException("El campo número de factura es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El campo estado es obligatorio");
        if (entidad.Fecha < new DateTime(1900, 1, 1) || entidad.Fecha > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha no es válida");
        if (entidad.Total < 0)
            throw new ValidacionException("El campo total no puede ser negativo");
        if (!conexion.proveedores!.Any(x => x.Id == entidad.Proveedor))
            throw new ValidacionException("El proveedor seleccionado no existe");
    }
}
