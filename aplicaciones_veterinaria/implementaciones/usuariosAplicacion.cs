using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;
namespace lib_aplicaciones.implementaciones;

public class usuariosAplicacion : IusuariosAplicacion
{
    private readonly IConexion conexion;

    public usuariosAplicacion(IConexion conexion)
    {
        this.conexion = conexion;
    }

    // Comprueba usuario y contrasena. La contrasena se compara cifrada (SHA-256).
    public sesionUsuario Login(credenciales entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.NombreUsuario) || string.IsNullOrEmpty(entidad.Contrasena))
            throw new ValidacionException("Ingrese el usuario y la contrasena");

        var clave = Cifrado.Sha256(entidad.Contrasena);
        var usuario = conexion.usuarios!.FirstOrDefault(x => x.NombreUsuario == entidad.NombreUsuario
            && x.Contrasena == clave && x.Estado == "Activo");

        if (usuario == null)
        {
            var intento = entidad.NombreUsuario.Length > 100 ? entidad.NombreUsuario.Substring(0, 100) : entidad.NombreUsuario;
            conexion.auditorias!.Add(new auditorias
            {
                Fecha = DateTime.Now,
                Accion = "INGRESO FALLIDO",
                Entidad = "usuarios",
                Detalle = "Usuario: " + intento
            });
            conexion.SaveChanges();
            throw new ValidacionException("Usuario o contrasena incorrectos");
        }

        var rol = conexion.roles!.First(x => x.Id == usuario.Rol);
        conexion.auditorias!.Add(new auditorias
        {
            Usuario = usuario.Id,
            Fecha = DateTime.Now,
            Accion = "INGRESO",
            Entidad = "usuarios",
            Registro = usuario.Id,
            Detalle = "Inicio de sesion"
        });
        conexion.SaveChanges();
        return new sesionUsuario { Id = usuario.Id, NombreUsuario = usuario.NombreUsuario, Rol = rol.Nombre };
    }

    public usuarios Actualizar(usuarios entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        Validar(entidad);
        // Contrasena vacia = se conserva la que ya tenia; con texto = se cifra la nueva
        if (string.IsNullOrWhiteSpace(entidad.Contrasena))
            entidad.Contrasena = conexion.usuarios!.Where(x => x.Id == entidad.Id).Select(x => x.Contrasena).First();
        else
            entidad.Contrasena = Cifrado.Sha256(entidad.Contrasena);
        var entry = conexion.Entry<usuarios>(entidad);
        entry.State = EntityState.Modified;
        conexion.SaveChanges();
        entidad.Contrasena = "";
        return entidad;
    }

    public usuarios Borrar(usuarios entidad)
    {
        if (entidad.Id == 0)
            throw new Exception("No se ha guardado");
        if (conexion.auditorias!.Any(x => x.Usuario == entidad.Id))
            throw new ValidacionException("No se puede borrar un usuario con registros de auditoria. Cambie su estado a Inactivo");
        conexion.usuarios!.Remove(entidad);
        conexion.SaveChanges();
        entidad.Contrasena = "";
        return entidad;
    }

    public List<usuarios> Buscar(usuarios entidad)
    {
        // El cuadro de busqueda de la pagina envia su texto en NombreUsuario: se usa como palabra clave
        var palabra = entidad.NombreUsuario;
        if (string.IsNullOrWhiteSpace(palabra))
            return Consultar();

        return Ocultar(conexion.usuarios!
            .Where(x => x.NombreUsuario!.Contains(palabra) || x.Estado!.Contains(palabra))
            .Take(100)
            .ToList());
    }

    public List<usuarios> Consultar() => Ocultar(conexion.usuarios!.Take(100).ToList());

    public usuarios Insert(usuarios entidad)
    {
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");
        Validar(entidad);
        entidad.Contrasena = Cifrado.Sha256(entidad.Contrasena!);
        conexion.usuarios!.Add(entidad);
        conexion.SaveChanges();
        entidad.Contrasena = "";
        return entidad;
    }

    // Nunca se devuelve la contrasena cifrada a la pagina
    private static List<usuarios> Ocultar(List<usuarios> lista)
    {
        foreach (var usuario in lista)
            usuario.Contrasena = "";
        return lista;
    }

    private void Validar(usuarios entidad)
    {
        if (string.IsNullOrWhiteSpace(entidad.NombreUsuario))
            throw new ValidacionException("El campo nombre de usuario es obligatorio");
        if (string.IsNullOrWhiteSpace(entidad.Estado))
            throw new ValidacionException("El campo estado es obligatorio");
        if (entidad.Id == 0 && string.IsNullOrWhiteSpace(entidad.Contrasena))
            throw new ValidacionException("El campo contrasena es obligatorio");
        if (!conexion.roles!.Any(x => x.Id == entidad.Rol))
            throw new ValidacionException("El rol seleccionado no existe");
        if (conexion.usuarios!.Any(x => x.NombreUsuario == entidad.NombreUsuario && x.Id != entidad.Id))
            throw new ValidacionException("Ya existe un usuario con ese nombre");
    }
}
