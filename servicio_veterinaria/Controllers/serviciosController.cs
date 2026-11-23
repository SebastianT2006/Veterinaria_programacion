using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class serviciosController : ControllerBase
{
    private readonly IserviciosAplicacion aplicacion; public serviciosController(IserviciosAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<servicios> Consultar() => aplicacion.Consultar(); [HttpPost] public List<servicios> Buscar(servicios e) => aplicacion.Buscar(e); [HttpPost] public servicios Insertar(servicios e) => aplicacion.Insert(e); [HttpPost] public servicios Actualizar(servicios e) => aplicacion.Actualizar(e); [HttpPost] public servicios Borrar(servicios e) => aplicacion.Borrar(e);
}
