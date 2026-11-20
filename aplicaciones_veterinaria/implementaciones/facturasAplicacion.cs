using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class facturasAplicacion : IfacturasAplicacion
{
    private readonly IConexion conexion;
    public facturasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public facturas Actualizar(facturas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<facturas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public facturas Borrar(facturas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.facturas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<facturas> Buscar(facturas entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en MetodoPago: se usa como palabra clave
        var palabra = entidad.MetodoPago;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.facturas!
            .Where(x => x.MetodoPago!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<facturas> Consultar() => conexion.facturas!.Take(100).ToList();
    public facturas Insert(facturas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.facturas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(facturas entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.MetodoPago))
            throw new ValidacionException("El campo método de pago es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El campo estado es obligatorio");
        if (entidad.Fecha < new DateTime(1900, 1, 1) || entidad.Fecha > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha no es válida");
        if (entidad.Total < 0)
            throw new ValidacionException("El campo total no puede ser negativo");
        if (!conexion.clientes!.Any(x => x.Id == entidad.Cliente))
            throw new ValidacionException("El cliente seleccionado no existe");
    }
}
