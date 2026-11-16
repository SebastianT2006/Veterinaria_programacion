using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
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
        if (Validar(entidad))
            throw new Exception("Falta información");
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
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Motivo)
            && string.IsNullOrWhiteSpace(entidad.Estado)
            && string.IsNullOrWhiteSpace(entidad.Observaciones))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Motivo) && x.Motivo != null && x.Motivo.Contains(entidad.Motivo!)) || (!string.IsNullOrWhiteSpace(entidad.Estado) && x.Estado != null && x.Estado.Contains(entidad.Estado!)) || (!string.IsNullOrWhiteSpace(entidad.Observaciones) && x.Observaciones != null && x.Observaciones.Contains(entidad.Observaciones!))).Take(100).ToList();
    }
    public List<citas> Consultar() => conexion.citas!.Take(100).ToList();
    public citas Insert(citas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.citas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(citas entidad) => string.IsNullOrEmpty(entidad.Motivo) || string.IsNullOrEmpty(entidad.Estado);
}
