using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class tratamientosController : ControllerBase
{
    private readonly ItratamientosAplicacion aplicacion; public tratamientosController(ItratamientosAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<tratamientos> Consultar() => aplicacion.Consultar(); [HttpPost] public List<tratamientos> Buscar(tratamientos e) => aplicacion.Buscar(e); [HttpPost] public tratamientos Insertar(tratamientos e) => aplicacion.Insert(e); [HttpPost] public tratamientos Actualizar(tratamientos e) => aplicacion.Actualizar(e); [HttpPost] public tratamientos Borrar(tratamientos e) => aplicacion.Borrar(e);
}
