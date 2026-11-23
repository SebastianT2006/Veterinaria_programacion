using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class productosAplicacion : IproductosAplicacion
{
    private readonly IConexion conexion;
    public productosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public productos Actualizar(productos entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<productos>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public productos Borrar(productos entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.productos!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<productos> Buscar(productos entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.productos!
            .Where(x => x.Nombre!.Contains(palabra) || x.Tipo!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<productos> Consultar() => conexion.productos!.Take(100).ToList();
    public productos Insert(productos entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");
        Validar(entidad);
        conexion.productos!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(productos entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El campo nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Tipo))
            throw new ValidacionException("El campo tipo es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Descripcion))
            throw new ValidacionException("El campo descripcion es obligatorio");
        if (entidad.Precio < 0)
            throw new ValidacionException("El campo precio no puede ser negativo");
        if (entidad.Stock < 0)
            throw new ValidacionException("El campo stock no puede ser negativo");
    }
}
