using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class empleadosAplicacion : IempleadosAplicacion
{
    private readonly IConexion conexion;
    public empleadosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public empleados Actualizar(empleados entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<empleados>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public empleados Borrar(empleados entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.empleados!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<empleados> Buscar(empleados entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Cargo: se usa como palabra clave
        var palabra = entidad.Cargo;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.empleados!
            .Where(x => x.Cargo!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<empleados> Consultar() => conexion.empleados!.Take(100).ToList();
    public empleados Insert(empleados entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");
        Validar(entidad);
        conexion.empleados!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(empleados entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.Cargo))
            throw new ValidacionException("El campo cargo es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El campo estado es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Turno))
            throw new ValidacionException("El campo turno es obligatorio");
        if (entidad.FechaIngreso < new DateTime(1900, 1, 1) || entidad.FechaIngreso > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha de ingreso no es valida");
        if (entidad.Salario < 0)
            throw new ValidacionException("El campo salario no puede ser negativo");
        if (!conexion.personas!.Any(x => x.Id == entidad.Persona))
            throw new ValidacionException("La persona seleccionada no existe");
        if (conexion.empleados!.Any(x => x.Persona == entidad.Persona && x.Id != entidad.Id))
            throw new ValidacionException("Esta persona ya esta registrada como empleado");
    }
}
