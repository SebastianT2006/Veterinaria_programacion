using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
namespace lib_aplicaciones.implementaciones;

public class auditoriasAplicacion : IauditoriasAplicacion
{
    private readonly IConexion conexion;

    public auditoriasAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }

    public List<auditorias> Consultar() => conexion.auditorias!
        .OrderByDescending(x => x.Id)
        .Take(100)
        .ToList();

    public List<auditorias> Buscar(auditorias entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en Accion: se usa como palabra clave
        var palabra = entidad.Accion;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        var usuariosQueCoinciden = conexion.usuarios!
            .Where(u => u.NombreUsuario!.Contains(palabra))
            .Select(u => u.Id)
            .ToList();

        return conexion.auditorias!
            .Where(x => x.Accion!.Contains(palabra) || x.Entidad!.Contains(palabra) || x.Detalle!.Contains(palabra)
                || (x.Usuario != null && usuariosQueCoinciden.Contains(x.Usuario.Value)))
            .OrderByDescending(x => x.Id)
            .Take(100)
            .ToList();
    }
}
