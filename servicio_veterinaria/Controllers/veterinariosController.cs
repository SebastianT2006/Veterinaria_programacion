using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class veterinariosController : ControllerBase
{
    private readonly IveterinariosAplicacion aplicacion; public veterinariosController(IveterinariosAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<veterinarios> Consultar() => aplicacion.Consultar(); [HttpPost] public List<veterinarios> Buscar(veterinarios e) => aplicacion.Buscar(e); [HttpPost] public veterinarios Insertar(veterinarios e) => aplicacion.Insert(e); [HttpPost] public veterinarios Actualizar(veterinarios e) => aplicacion.Actualizar(e); [HttpPost] public veterinarios Borrar(veterinarios e) => aplicacion.Borrar(e);
}
