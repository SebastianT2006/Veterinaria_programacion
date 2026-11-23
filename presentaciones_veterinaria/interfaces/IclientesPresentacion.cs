using lib_aplicaciones.entidades;
namespace presentaciones_veterinaria.interfaces;

public interface IclientesPresentacion
{
    List<clientes> Consultar(); List<clientes> Buscar(clientes entidad); clientes Insertar(clientes entidad); clientes Actualizar(clientes entidad); clientes Borrar(clientes entidad);
}
