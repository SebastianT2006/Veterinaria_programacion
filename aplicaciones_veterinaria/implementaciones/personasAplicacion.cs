using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class personasAplicacion : IpersonasAplicacion
{
    private readonly IConexion conexion;
    public personasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public personas Actualizar(personas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<personas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public personas Borrar(personas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.personas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<personas> Buscar(personas entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.personas!
            .Where(x => x.Nombre!.Contains(palabra) || x.Email!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<personas> Consultar() => conexion.personas!.Take(100).ToList();
    public personas Insert(personas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.personas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(personas entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El campo nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Telefono))
            throw new ValidacionException("El campo teléfono es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Email))
            throw new ValidacionException("El campo correo electrónico es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Direccion))
            throw new ValidacionException("El campo dirección es obligatorio");
        if (entidad.FechaNacimiento < new DateTime(1900, 1, 1) || entidad.FechaNacimiento > DateTime.Today)
            throw new ValidacionException("La fecha de nacimiento no es válida");
    }
}
