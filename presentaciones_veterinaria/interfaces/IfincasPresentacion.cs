using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IfincasPresentacion
{
    List<fincas> Consultar(); List<fincas> Buscar(fincas entidad); fincas Insertar(fincas entidad); fincas Actualizar(fincas entidad); fincas Borrar(fincas entidad);
}
