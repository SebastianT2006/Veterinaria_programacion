using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class animalesAplicacion : IanimalesAplicacion
{
    private readonly IConexion conexion;
    public animalesAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public animales Actualizar(animales entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<animales>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public animales Borrar(animales entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.animales!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<animales> Buscar(animales entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.animales!
            .Where(x => x.Nombre!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<animales> Consultar() => conexion.animales!.Take(100).ToList();
    public animales Insert(animales entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");
        Validar(entidad);
        conexion.animales!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(animales entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Sexo))
            throw new ValidacionException("El sexo es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Color))
            throw new ValidacionException("El color es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El estado es obligatorio");
        if (entidad.FechaNacimiento < new DateTime(1900, 1, 1) || entidad.FechaNacimiento > DateTime.Today)
            throw new ValidacionException("La fecha de nacimiento no es valida");
        if (entidad.Peso <= 0)
            throw new ValidacionException("El peso debe ser mayor que cero");
        if (!conexion.razas!.Any(x => x.Id == entidad.Raza))
            throw new ValidacionException("La raza seleccionada no existe");
        if (!conexion.clientes!.Any(x => x.Id == entidad.Cliente))
            throw new ValidacionException("El cliente seleccionado no existe");
        if (!conexion.fincas!.Any(x => x.Id == entidad.Finca))
            throw new ValidacionException("La finca seleccionada no existe");
    }
}
