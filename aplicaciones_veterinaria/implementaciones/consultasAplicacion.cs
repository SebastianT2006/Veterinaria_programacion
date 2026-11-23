using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class consultasAplicacion : IconsultasAplicacion
{
    private readonly IConexion conexion;
    public consultasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public consultas Actualizar(consultas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<consultas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public consultas Borrar(consultas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.consultas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<consultas> Buscar(consultas entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Diagnostico: se usa como palabra clave
        var palabra = entidad.Diagnostico;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.consultas!
            .Where(x => x.Diagnostico!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<consultas> Consultar() => conexion.consultas!.Take(100).ToList();
    public consultas Insert(consultas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");
        Validar(entidad);
        conexion.consultas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(consultas entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Diagnostico))
            throw new ValidacionException("El campo diagnostico es obligatorio");
        if (entidad.Fecha < new DateTime(1900, 1, 1) || entidad.Fecha > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha no es valida");
        if (entidad.PesoActual <= 0)
            throw new ValidacionException("El campo peso actual debe ser mayor que cero");
        if (!conexion.citas!.Any(x => x.Id == entidad.Cita))
            throw new ValidacionException("La cita seleccionada no existe");
        if (!conexion.animales!.Any(x => x.Id == entidad.Animal))
            throw new ValidacionException("El animal seleccionado no existe");
        if (!conexion.veterinarios!.Any(x => x.Id == entidad.Veterinario))
            throw new ValidacionException("El veterinario seleccionado no existe");
        if (conexion.consultas!.Any(x => x.Cita == entidad.Cita && x.Id != entidad.Id))
            throw new ValidacionException("Esta cita ya tiene una consulta registrada");
    }
}
