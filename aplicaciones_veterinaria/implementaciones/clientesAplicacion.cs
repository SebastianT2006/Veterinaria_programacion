using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class clientesAplicacion : IclientesAplicacion
{
    private readonly IConexion conexion;
    public clientesAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public clientes Actualizar(clientes entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<clientes>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public clientes Borrar(clientes entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.clientes!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<clientes> Buscar(clientes entidad)
    {
        var lista = Consultar();

        if (string.IsNullOrWhiteSpace(entidad.TipoCliente)
            && string.IsNullOrWhiteSpace(entidad.Estado)
            && string.IsNullOrWhiteSpace(entidad.Observaciones)
            && string.IsNullOrWhiteSpace(entidad.PreferenciaContacto))
            return lista;

        return lista.Where(x =>
               (!string.IsNullOrWhiteSpace(entidad.TipoCliente) && x.TipoCliente != null && x.TipoCliente.Contains(entidad.TipoCliente!))
            || (!string.IsNullOrWhiteSpace(entidad.Estado) && x.Estado != null && x.Estado.Contains(entidad.Estado!))
            || (!string.IsNullOrWhiteSpace(entidad.Observaciones) && x.Observaciones != null && x.Observaciones.Contains(entidad.Observaciones!))
            || (!string.IsNullOrWhiteSpace(entidad.PreferenciaContacto) && x.PreferenciaContacto != null && x.PreferenciaContacto.Contains(entidad.PreferenciaContacto!))
        ).Take(100).ToList();
    }
    public List<clientes> Consultar() => conexion.clientes!.Take(100).ToList();
    public clientes Insert(clientes entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.clientes!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(clientes entidad) => string.IsNullOrEmpty(entidad.TipoCliente) || string.IsNullOrEmpty(entidad.Estado) || string.IsNullOrEmpty(entidad.PreferenciaContacto);
}
