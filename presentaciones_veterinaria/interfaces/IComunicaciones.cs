namespace presentaciones_veterinaria.interfaces;

public interface IComunicaciones
{
    Task<T> Ejecutar<T>(string url, object? entidad);
}
