using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IcomprasPresentacion
{
    List<compras> Consultar(); List<compras> Buscar(compras entidad); compras Insertar(compras entidad); compras Actualizar(compras entidad); compras Borrar(compras entidad);
}
