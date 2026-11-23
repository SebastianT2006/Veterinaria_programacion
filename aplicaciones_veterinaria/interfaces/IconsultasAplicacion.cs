using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IconsultasAplicacion
{
    consultas Insert(consultas entidad); List<consultas> Consultar(); List<consultas> Buscar(consultas entidad); consultas Actualizar(consultas entidad); consultas Borrar(consultas entidad);
}
