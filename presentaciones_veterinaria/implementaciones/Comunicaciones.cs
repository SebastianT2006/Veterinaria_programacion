using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using presentaciones_veterinaria.interfaces;
using System.Text;
namespace presentaciones_veterinaria.implementaciones;

public class Comunicaciones : IComunicaciones
{
    private readonly IHttpContextAccessor? accesor;
    private readonly HttpMessageHandler? manejador;

    public Comunicaciones()
    {
    }

    // La pagina (front) entrega el accesor para poder leer el usuario de la sesion
    public Comunicaciones(IHttpContextAccessor accesor)
    {
        this.accesor = accesor;
    }

    // Solo para las pruebas: permite entregar una respuesta falsa en vez de llamar a la API
    public Comunicaciones(IHttpContextAccessor? accesor, HttpMessageHandler manejador)
    {
        this.accesor = accesor;
        this.manejador = manejador;
    }

    public async Task<T> Ejecutar<T>(string url, object? entidad)
    {
        var baseUrl = Environment.GetEnvironmentVariable("VETERINARIA_API_URL") ?? "http://localhost:5209/";
        using var cliente = manejador == null ? new HttpClient() : new HttpClient(manejador, false);
        cliente.Timeout = TimeSpan.FromMinutes(4);
        var idUsuario = accesor?.HttpContext?.Session.GetString("UsuarioId");
        if (!string.IsNullOrEmpty(idUsuario))
            cliente.DefaultRequestHeaders.Add("X-Usuario", idUsuario);
        var body = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");
        var response = await cliente.PostAsync(new Uri(new Uri(baseUrl), url), body);
        if ((int)response.StatusCode == 422)
            throw new ValidacionException(await response.Content.ReadAsStringAsync());
        if (!response.IsSuccessStatusCode)
            throw new Exception("Error Comunicación");
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
    }
}
