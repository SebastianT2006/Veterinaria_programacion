using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class rolesAplicacion : IrolesAplicacion
{
    private readonly IConexion conexion;

    public rolesAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }

    public roles Actualizar(roles entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<roles>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }

    public roles Borrar(roles entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (conexion.usuarios!.Any(x => x.Rol == entidad.Id))
            throw new ValidacionException("No se puede borrar un rol que tiene usuarios");
        conexion.roles!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }

    public List<roles> Buscar(roles entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.roles!
            .Where(x => x.Nombre!.Contains(palabra))
            .Take(100)
            .ToList();
    }

    public List<roles> Consultar() => conexion.roles!.Take(100).ToList();

    public roles Insert(roles entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");
        Validar(entidad);
        conexion.roles!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }

    private void Validar(roles entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El campo nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Descripcion))
            throw new ValidacionException("El campo descripcion es obligatorio");
        if (conexion.roles!.Any(x => x.Nombre == entidad.Nombre && x.Id != entidad.Id))
            throw new ValidacionException("Ya existe un rol con ese nombre");
    }
}
