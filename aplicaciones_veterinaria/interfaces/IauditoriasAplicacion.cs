using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IauditoriasAplicacion
{
    List<auditorias> Consultar();
    List<auditorias> Buscar(auditorias entidad);
}
