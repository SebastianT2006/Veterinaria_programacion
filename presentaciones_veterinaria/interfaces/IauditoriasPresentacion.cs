using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IauditoriasPresentacion
{
    List<auditorias> Consultar();
    List<auditorias> Buscar(auditorias entidad);
}
