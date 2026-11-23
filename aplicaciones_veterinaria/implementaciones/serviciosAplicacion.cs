using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class serviciosAplicacion : IserviciosAplicacion
{
    private readonly IConexion conexion;
    public serviciosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public servicios Actualizar(servicios entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<servicios>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public servicios Borrar(servicios entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.servicios!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<servicios> Buscar(servicios entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.servicios!
            .Where(x => x.Nombre!.Contains(palabra) || x.Categoria!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<servicios> Consultar() => conexion.servicios!.Take(100).ToList();
    public servicios Insert(servicios entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.servicios!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(servicios entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El campo nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Descripcion))
            throw new ValidacionException("El campo descripción es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Categoria))
            throw new ValidacionException("El campo categoría es obligatorio");
        if (entidad.Precio < 0)
            throw new ValidacionException("El campo precio no puede ser negativo");
        if (entidad.Duracion <= 0)
            throw new ValidacionException("El campo duración debe ser mayor que cero");
    }
}
