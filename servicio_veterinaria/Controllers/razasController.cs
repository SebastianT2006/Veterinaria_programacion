using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class razasController : ControllerBase
{
    private readonly IrazasAplicacion aplicacion; public razasController(IrazasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<razas> Consultar() => aplicacion.Consultar(); [HttpPost] public List<razas> Buscar(razas e) => aplicacion.Buscar(e); [HttpPost] public razas Insertar(razas e) => aplicacion.Insert(e); [HttpPost] public razas Actualizar(razas e) => aplicacion.Actualizar(e); [HttpPost] public razas Borrar(razas e) => aplicacion.Borrar(e);
}
