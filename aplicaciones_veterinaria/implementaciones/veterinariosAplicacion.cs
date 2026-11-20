using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class veterinariosAplicacion : IveterinariosAplicacion
{
    private readonly IConexion conexion;
    public veterinariosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public veterinarios Actualizar(veterinarios entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<veterinarios>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public veterinarios Borrar(veterinarios entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.veterinarios!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<veterinarios> Buscar(veterinarios entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Especialidad: se usa como palabra clave
        var palabra = entidad.Especialidad;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.veterinarios!
            .Where(x => x.Especialidad!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<veterinarios> Consultar() => conexion.veterinarios!.Take(100).ToList();
    public veterinarios Insert(veterinarios entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.veterinarios!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(veterinarios entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Especialidad))
            throw new ValidacionException("El campo especialidad es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.RegistroProfesional))
            throw new ValidacionException("El campo registro profesional es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El campo estado es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.TelefonoLaboral))
            throw new ValidacionException("El campo teléfono laboral es obligatorio");
        if (entidad.FechaIngreso < new DateTime(1900, 1, 1) || entidad.FechaIngreso > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha de ingreso no es válida");
        if (!conexion.personas!.Any(x => x.Id == entidad.Persona))
            throw new ValidacionException("La persona seleccionada no existe");
        if (conexion.veterinarios!.Any(x => x.Persona == entidad.Persona && x.Id != entidad.Id))
            throw new ValidacionException("Esta persona ya está registrada como veterinario");
    }
}
