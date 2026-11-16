using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
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
        if (Validar(entidad))
            throw new Exception("Falta información");
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
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.MetodoPago)
            && string.IsNullOrWhiteSpace(entidad.Estado)
            && string.IsNullOrWhiteSpace(entidad.Observaciones))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.MetodoPago) && x.MetodoPago != null && x.MetodoPago.Contains(entidad.MetodoPago!)) || (!string.IsNullOrWhiteSpace(entidad.Estado) && x.Estado != null && x.Estado.Contains(entidad.Estado!)) || (!string.IsNullOrWhiteSpace(entidad.Observaciones) && x.Observaciones != null && x.Observaciones.Contains(entidad.Observaciones!))).Take(100).ToList();
    }
    public List<facturas> Consultar() => conexion.facturas!.Take(100).ToList();
    public facturas Insert(facturas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.facturas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(facturas entidad) => string.IsNullOrEmpty(entidad.MetodoPago) || string.IsNullOrEmpty(entidad.Estado);
}
