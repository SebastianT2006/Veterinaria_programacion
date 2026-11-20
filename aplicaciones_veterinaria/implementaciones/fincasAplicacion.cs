using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class fincasAplicacion : IfincasAplicacion
{
    private readonly IConexion conexion;
    public fincasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public fincas Actualizar(fincas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<fincas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public fincas Borrar(fincas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.fincas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<fincas> Buscar(fincas entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.fincas!
            .Where(x => x.Nombre!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<fincas> Consultar() => conexion.fincas!.Take(100).ToList();
    public fincas Insert(fincas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.fincas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(fincas entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El campo nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Direccion))
            throw new ValidacionException("El campo dirección es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.TipoProduccion))
            throw new ValidacionException("El campo tipo de producción es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El campo estado es obligatorio");
        if (entidad.AreaHectareas <= 0)
            throw new ValidacionException("El campo área en hectáreas debe ser mayor que cero");
        if (!conexion.clientes!.Any(x => x.Id == entidad.Cliente))
            throw new ValidacionException("El cliente seleccionado no existe");
    }
}
