using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class proveedoresAplicacion : IproveedoresAplicacion
{
    private readonly IConexion conexion;
    public proveedoresAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public proveedores Actualizar(proveedores entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<proveedores>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public proveedores Borrar(proveedores entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.proveedores!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<proveedores> Buscar(proveedores entidad)
    {
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Nombre)
            && string.IsNullOrWhiteSpace(entidad.Telefono)
            && string.IsNullOrWhiteSpace(entidad.Email)
            && string.IsNullOrWhiteSpace(entidad.Direccion)
            && string.IsNullOrWhiteSpace(entidad.Nit))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Nombre) && x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) || (!string.IsNullOrWhiteSpace(entidad.Telefono) && x.Telefono != null && x.Telefono.Contains(entidad.Telefono!)) || (!string.IsNullOrWhiteSpace(entidad.Email) && x.Email != null && x.Email.Contains(entidad.Email!)) || (!string.IsNullOrWhiteSpace(entidad.Direccion) && x.Direccion != null && x.Direccion.Contains(entidad.Direccion!)) || (!string.IsNullOrWhiteSpace(entidad.Nit) && x.Nit != null && x.Nit.Contains(entidad.Nit!))).Take(100).ToList();
    }
    public List<proveedores> Consultar() => conexion.proveedores!.Take(100).ToList();
    public proveedores Insert(proveedores entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.proveedores!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(proveedores entidad) => string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Telefono) || string.IsNullOrEmpty(entidad.Email) || string.IsNullOrEmpty(entidad.Direccion) || string.IsNullOrEmpty(entidad.Nit);
}
