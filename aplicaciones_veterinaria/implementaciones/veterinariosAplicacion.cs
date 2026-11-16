using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
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
        if (Validar(entidad))
            throw new Exception("Falta información");
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
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Especialidad)
            && string.IsNullOrWhiteSpace(entidad.RegistroProfesional)
            && string.IsNullOrWhiteSpace(entidad.Estado)
            && string.IsNullOrWhiteSpace(entidad.TelefonoLaboral))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Especialidad) && x.Especialidad != null && x.Especialidad.Contains(entidad.Especialidad!)) || (!string.IsNullOrWhiteSpace(entidad.RegistroProfesional) && x.RegistroProfesional != null && x.RegistroProfesional.Contains(entidad.RegistroProfesional!)) || (!string.IsNullOrWhiteSpace(entidad.Estado) && x.Estado != null && x.Estado.Contains(entidad.Estado!)) || (!string.IsNullOrWhiteSpace(entidad.TelefonoLaboral) && x.TelefonoLaboral != null && x.TelefonoLaboral.Contains(entidad.TelefonoLaboral!))).Take(100).ToList();
    }
    public List<veterinarios> Consultar() => conexion.veterinarios!.Take(100).ToList();
    public veterinarios Insert(veterinarios entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.veterinarios!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(veterinarios entidad) => string.IsNullOrEmpty(entidad.Especialidad) || string.IsNullOrEmpty(entidad.RegistroProfesional) || string.IsNullOrEmpty(entidad.Estado) || string.IsNullOrEmpty(entidad.TelefonoLaboral);
}
