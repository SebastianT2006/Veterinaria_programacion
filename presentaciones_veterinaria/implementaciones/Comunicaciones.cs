using lib_aplicaciones.nucleo;
using Newtonsoft.Json;
using presentaciones_veterinaria.interfaces;
using System.Text;
namespace presentaciones_veterinaria.implementaciones;

public class Comunicaciones : IComunicaciones
{
    public async Task<T> Ejecutar<T>(string url, object? entidad)
    {
        var baseUrl = Environment.GetEnvironmentVariable("VETERINARIA_API_URL") ?? "http://localhost:5209/";
        using var cliente = new HttpClient { Timeout = TimeSpan.FromMinutes(4) };
        var body = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");
        var response = await cliente.PostAsync(new Uri(new Uri(baseUrl), url), body);
        if ((int)response.StatusCode == 422)
            throw new ValidacionException(await response.Content.ReadAsStringAsync());
        if (!response.IsSuccessStatusCode)
            throw new Exception("Error Comunicación");
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
    }
}
