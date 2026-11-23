using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class vacunacionesController : ControllerBase
{
    private readonly IvacunacionesAplicacion aplicacion; public vacunacionesController(IvacunacionesAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<vacunaciones> Consultar() => aplicacion.Consultar(); [HttpPost] public List<vacunaciones> Buscar(vacunaciones e) => aplicacion.Buscar(e); [HttpPost] public vacunaciones Insertar(vacunaciones e) => aplicacion.Insert(e); [HttpPost] public vacunaciones Actualizar(vacunaciones e) => aplicacion.Actualizar(e); [HttpPost] public vacunaciones Borrar(vacunaciones e) => aplicacion.Borrar(e);
}
