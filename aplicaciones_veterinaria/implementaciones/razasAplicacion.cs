using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class razasAplicacion : IrazasAplicacion
{
    private readonly IConexion conexion;
    public razasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public razas Actualizar(razas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<razas>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public razas Borrar(razas entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.razas!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<razas> Buscar(razas entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Nombre: se usa como palabra clave
        var palabra = entidad.Nombre;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.razas!
            .Where(x => x.Nombre!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<razas> Consultar() => conexion.razas!.Take(100).ToList();
    public razas Insert(razas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.razas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(razas entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Nombre))
            throw new ValidacionException("El campo nombre es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Descripcion))
            throw new ValidacionException("El campo descripción es obligatorio");
        if (entidad.TamanoPromedio <= 0)
            throw new ValidacionException("El campo tamaño promedio debe ser mayor que cero");
        if (entidad.PesoPromedio <= 0)
            throw new ValidacionException("El campo peso promedio debe ser mayor que cero");
        if (entidad.EsperanzaVidaAnios <= 0)
            throw new ValidacionException("El campo esperanza de vida (años) debe ser mayor que cero");
        if (!conexion.especies!.Any(x => x.Id == entidad.Especie))
            throw new ValidacionException("La especie seleccionada no existe");
    }
}
