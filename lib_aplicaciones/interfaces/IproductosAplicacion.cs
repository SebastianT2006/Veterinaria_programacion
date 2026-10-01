using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IproductosAplicacion
    {
        productos Insert(productos entidad);
        List<productos> Consultar();
        productos Actualizar(productos entidad);
        productos Borrar(productos entidad);
    }
}