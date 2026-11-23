using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IcitasPresentacion
{
    List<citas> Consultar(); List<citas> Buscar(citas entidad); citas Insertar(citas entidad); citas Actualizar(citas entidad); citas Borrar(citas entidad);
}
