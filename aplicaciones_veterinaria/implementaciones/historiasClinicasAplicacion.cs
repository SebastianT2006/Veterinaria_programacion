using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
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
        Validar(entidad);
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
        // El cuadro de busqueda de la pagina envia su texto en Antecedentes: se usa como palabra clave
        var palabra = entidad.Antecedentes;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.historiasClinicas!
            .Where(x => x.Antecedentes!.Contains(palabra) || x.Alergias!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<historiasClinicas> Consultar() => conexion.historiasClinicas!.Take(100).ToList();
    public historiasClinicas Insert(historiasClinicas entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardó");
        Validar(entidad);
        conexion.historiasClinicas!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(historiasClinicas entidad)
    {
        if (!conexion.animales!.Any(x => x.Id == entidad.Animal))
            throw new ValidacionException("El animal seleccionado no existe");
        if (conexion.historiasClinicas!.Any(x => x.Animal == entidad.Animal && x.Id != entidad.Id))
            throw new ValidacionException("Este animal ya tiene una historia clínica");
    }
}
