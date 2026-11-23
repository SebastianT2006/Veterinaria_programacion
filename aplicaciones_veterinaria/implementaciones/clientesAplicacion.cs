using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class clientesAplicacion : IclientesAplicacion
{
    private readonly IConexion conexion;
    public clientesAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }
    public clientes Actualizar(clientes entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        var entry = conexion.Entry<clientes>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public clientes Borrar(clientes entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        conexion.clientes!.Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public List<clientes> Buscar(clientes entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en TipoCliente: se usa como palabra clave
        var palabra = entidad.TipoCliente;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return conexion.clientes!
            .Where(x => x.TipoCliente!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList();
    }
    public List<clientes> Consultar() => conexion.clientes!.Take(100).ToList();
    public clientes Insert(clientes entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");
        Validar(entidad);
        conexion.clientes!.Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    private void Validar(clientes entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.TipoCliente))
            throw new ValidacionException("El campo tipo de cliente es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El campo estado es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.PreferenciaContacto))
            throw new ValidacionException("El campo preferencia de contacto es obligatorio");
        if (entidad.FechaRegistro < new DateTime(1900, 1, 1) || entidad.FechaRegistro > new DateTime(2079, 6, 6))
            throw new ValidacionException("La fecha de registro no es valida");
        if (!conexion.personas!.Any(x => x.Id == entidad.Persona))
            throw new ValidacionException("La persona seleccionada no existe");
        if (conexion.clientes!.Any(x => x.Persona == entidad.Persona && x.Id != entidad.Id))
            throw new ValidacionException("Esta persona ya esta registrada como cliente");
    }
}
