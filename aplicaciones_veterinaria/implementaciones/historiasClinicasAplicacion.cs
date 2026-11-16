using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class historiasClinicasAplicacion : IhistoriasClinicasAplicacion
{
    private readonly IConexion conexion;
    public historiasClinicasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public historiasClinicas Actualizar(historiasClinicas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (Validar(entidad))
            throw new Exception("Falta información");
        var entry = conexion.Entry<historiasClinicas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public historiasClinicas Borrar(historiasClinicas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.historiasClinicas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<historiasClinicas> Buscar(historiasClinicas entidad)
    {
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Antecedentes)
            && string.IsNullOrWhiteSpace(entidad.Alergias)
            && string.IsNullOrWhiteSpace(entidad.Observaciones))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Antecedentes) && x.Antecedentes != null && x.Antecedentes.Contains(entidad.Antecedentes!)) || (!string.IsNullOrWhiteSpace(entidad.Alergias) && x.Alergias != null && x.Alergias.Contains(entidad.Alergias!)) || (!string.IsNullOrWhiteSpace(entidad.Observaciones) && x.Observaciones != null && x.Observaciones.Contains(entidad.Observaciones!))).Take(100).ToList();
    }
    public List<historiasClinicas> Consultar() => conexion.historiasClinicas!.Take(100).ToList();
    public historiasClinicas Insert(historiasClinicas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.historiasClinicas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(historiasClinicas entidad) => false;
}
