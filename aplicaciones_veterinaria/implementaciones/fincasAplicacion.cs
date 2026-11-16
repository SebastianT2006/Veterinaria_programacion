using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
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
        if (Validar(entidad))
            throw new Exception("Falta información");
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
        var lista = Consultar();
        if (string.IsNullOrWhiteSpace(entidad.Nombre)
            && string.IsNullOrWhiteSpace(entidad.Direccion)
            && string.IsNullOrWhiteSpace(entidad.TipoProduccion)
            && string.IsNullOrWhiteSpace(entidad.Estado))
            return lista;

        return lista.Where(x => (!string.IsNullOrWhiteSpace(entidad.Nombre) && x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) || (!string.IsNullOrWhiteSpace(entidad.Direccion) && x.Direccion != null && x.Direccion.Contains(entidad.Direccion!)) || (!string.IsNullOrWhiteSpace(entidad.TipoProduccion) && x.TipoProduccion != null && x.TipoProduccion.Contains(entidad.TipoProduccion!)) || (!string.IsNullOrWhiteSpace(entidad.Estado) && x.Estado != null && x.Estado.Contains(entidad.Estado!))).Take(100).ToList();
    }
    public List<fincas> Consultar() => conexion.fincas!.Take(100).ToList();
    public fincas Insert(fincas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        if (Validar(entidad))
            throw new Exception("Falta información");
        conexion.fincas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private bool Validar(fincas entidad) => string.IsNullOrEmpty(entidad.Nombre) || string.IsNullOrEmpty(entidad.Direccion) || string.IsNullOrEmpty(entidad.TipoProduccion) || string.IsNullOrEmpty(entidad.Estado);
}
