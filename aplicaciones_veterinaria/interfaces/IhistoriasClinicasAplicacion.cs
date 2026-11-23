using lib_aplicaciones.entidades;
namespace lib_aplicaciones.interfaces;

public interface IhistoriasClinicasAplicacion
{
    historiasClinicas Insert(historiasClinicas entidad); List<historiasClinicas> Consultar(); List<historiasClinicas> Buscar(historiasClinicas entidad); historiasClinicas Actualizar(historiasClinicas entidad); historiasClinicas Borrar(historiasClinicas entidad);
}
