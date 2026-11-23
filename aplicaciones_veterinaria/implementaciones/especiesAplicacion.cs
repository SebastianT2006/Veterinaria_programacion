using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class especiesAplicacion : IespeciesAplicacion
{
    private readonly IConexion conexion;
    public especiesAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public especies Actualizar(especies entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<especies>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public especies Borrar(especies entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.especies!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<especies> Buscar(especies entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.especies!
            .Where(x => x.Nombre!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<especies> Consultar() => conexion.especies!.Take(100).ToList();
    public especies Insert(especies entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");
        Validar(entidad);
        conexion.especies!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(especies entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El campo nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Descripcion))
            throw new ValidacionException("El campo descripcion es obligatorio");
    }
}
