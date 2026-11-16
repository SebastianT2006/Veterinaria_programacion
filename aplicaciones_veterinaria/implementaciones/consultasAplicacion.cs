using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
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
        if (Validar(entidad))
            throw new Exception("Falta información");
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
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Diagnostico)
            && string.IsNullOrWhiteSpace(entidad.Observaciones))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Diagnostico) && x.Diagnostico != null && x.Diagnostico.Contains(entidad.Diagnostico!)) || (!string.IsNullOrWhiteSpace(entidad.Observaciones) && x.Observaciones != null && x.Observaciones.Contains(entidad.Observaciones!))).Take(100).ToList();
    }
    public List<consultas> Consultar() => conexion.consultas!.Take(100).ToList();
    public consultas Insert(consultas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.consultas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(consultas entidad) => string.IsNullOrEmpty(entidad.Diagnostico);
}
