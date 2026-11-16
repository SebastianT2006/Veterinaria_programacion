using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
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
        if (Validar(entidad))
            throw new Exception("Falta información");
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
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.NumeroFactura)
            && string.IsNullOrWhiteSpace(entidad.Estado)
            && string.IsNullOrWhiteSpace(entidad.Observaciones))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.NumeroFactura) && x.NumeroFactura != null && x.NumeroFactura.Contains(entidad.NumeroFactura!)) || (!string.IsNullOrWhiteSpace(entidad.Estado) && x.Estado != null && x.Estado.Contains(entidad.Estado!)) || (!string.IsNullOrWhiteSpace(entidad.Observaciones) && x.Observaciones != null && x.Observaciones.Contains(entidad.Observaciones!))).Take(100).ToList();
    }
    public List<compras> Consultar() => conexion.compras!.Take(100).ToList();
    public compras Insert(compras entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.compras!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(compras entidad) => string.IsNullOrEmpty(entidad.NumeroFactura) || string.IsNullOrEmpty(entidad.Estado);
}
