using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class personasController : ControllerBase
{
    private readonly IpersonasAplicacion aplicacion; public personasController(IpersonasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<personas> Consultar() => aplicacion.Consultar(); [HttpPost] public List<personas> Buscar(personas e) => aplicacion.Buscar(e); [HttpPost] public personas Insertar(personas e) => aplicacion.Insert(e); [HttpPost] public personas Actualizar(personas e) => aplicacion.Actualizar(e); [HttpPost] public personas Borrar(personas e) => aplicacion.Borrar(e);
}
