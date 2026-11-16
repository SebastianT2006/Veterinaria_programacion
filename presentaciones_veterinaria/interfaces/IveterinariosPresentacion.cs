using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IveterinariosPresentacion
{
    List<veterinarios> Consultar(); List<veterinarios> Buscar(veterinarios entidad); veterinarios Insertar(veterinarios entidad); veterinarios Actualizar(veterinarios entidad); veterinarios Borrar(veterinarios entidad);
}
