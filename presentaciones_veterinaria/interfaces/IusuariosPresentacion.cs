using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IusuariosPresentacion
{
    List<usuarios> Consultar();
    List<usuarios> Buscar(usuarios entidad);
    usuarios Insertar(usuarios entidad);
    usuarios Actualizar(usuarios entidad);
    usuarios Borrar(usuarios entidad);
    sesionUsuario Login(credenciales entidad);
}
