using lib_aplicaciones.entidades;

namespace lib_aplicaciones.interfaces
{
    public interface IveterinariosAplicacion
    {
        veterinarios Insert(veterinarios entidad);
        List<veterinarios> Consultar();
        veterinarios Actualizar(veterinarios entidad);
        veterinarios Borrar(veterinarios entidad);
    }
}