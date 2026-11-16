using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IhistoriasClinicasPresentacion
{
    List<historiasClinicas> Consultar(); List<historiasClinicas> Buscar(historiasClinicas entidad); historiasClinicas Insertar(historiasClinicas entidad); historiasClinicas Actualizar(historiasClinicas entidad); historiasClinicas Borrar(historiasClinicas entidad);
}
