using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class citasAplicacion : IcitasAplicacion
{
    private readonly IConexion conexion;
    public citasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public citas Actualizar(citas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<citas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public citas Borrar(citas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.citas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<citas> Buscar(citas entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Motivo: se usa como palabra clave
        var palabra = entidad.Motivo;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.citas!
            .Where(x => x.Motivo!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<citas> Consultar() => conexion.citas!.Take(100).ToList();
    public citas Insert(citas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.citas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(citas entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Motivo))
            throw new ValidacionException("El campo motivo es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El campo estado es obligatorio");
        if (entidad.Fecha < new DateTime(1900, 1, 1) || entidad.Fecha > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha no es válida");
        if (!conexion.animales!.Any(x => x.Id == entidad.Animal))
            throw new ValidacionException("El animal seleccionado no existe");
        if (!conexion.veterinarios!.Any(x => x.Id == entidad.Veterinario))
            throw new ValidacionException("El veterinario seleccionado no existe");
    }
}
