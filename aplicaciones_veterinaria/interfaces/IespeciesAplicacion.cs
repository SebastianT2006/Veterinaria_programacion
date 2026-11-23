using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IespeciesAplicacion
{
    especies Insert(especies entidad); List<especies> Consultar(); List<especies> Buscar(especies entidad); especies Actualizar(especies entidad); especies Borrar(especies entidad);
}
