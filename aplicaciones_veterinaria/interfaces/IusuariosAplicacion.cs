using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IusuariosAplicacion
{
    usuarios Insert(usuarios entidad);
    List<usuarios> Consultar();
    List<usuarios> Buscar(usuarios entidad);
    usuarios Actualizar(usuarios entidad);
    usuarios Borrar(usuarios entidad);
    sesionUsuario Login(credenciales entidad);
}
