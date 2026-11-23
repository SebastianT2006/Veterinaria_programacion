using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IconsultasPresentacion
{
    List<consultas> Consultar(); List<consultas> Buscar(consultas entidad); consultas Insertar(consultas entidad); consultas Actualizar(consultas entidad); consultas Borrar(consultas entidad);
}
