using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
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
        Validar(entidad);
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
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.proveedores!
            .Where(x => x.Nombre!.Contains(palabra) || x.Nit!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<proveedores> Consultar() => conexion.proveedores!.Take(100).ToList();
    public proveedores Insert(proveedores entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.proveedores!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(proveedores entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El campo nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Telefono))
            throw new ValidacionException("El campo teléfono es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Email))
            throw new ValidacionException("El campo correo electrónico es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Direccion))
            throw new ValidacionException("El campo dirección es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Nit))
            throw new ValidacionException("El campo NIT es obligatorio");
    }
}
