using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IproductosPresentacion
{
    List<productos> Consultar(); List<productos> Buscar(productos entidad); productos Insertar(productos entidad); productos Actualizar(productos entidad); productos Borrar(productos entidad);
}
