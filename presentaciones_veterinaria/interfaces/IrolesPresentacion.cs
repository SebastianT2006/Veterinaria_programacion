using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IrolesPresentacion
{
    List<roles> Consultar();
    List<roles> Buscar(roles entidad);
    roles Insertar(roles entidad);
    roles Actualizar(roles entidad);
    roles Borrar(roles entidad);
}
