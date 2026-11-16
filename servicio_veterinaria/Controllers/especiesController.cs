using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class especiesController : ControllerBase
{
    private readonly IespeciesAplicacion aplicacion; public especiesController(IespeciesAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<especies> Consultar() => aplicacion.Consultar(); [HttpPost] public List<especies> Buscar(especies e) => aplicacion.Buscar(e); [HttpPost] public especies Insertar(especies e) => aplicacion.Insert(e); [HttpPost] public especies Actualizar(especies e) => aplicacion.Actualizar(e); [HttpPost] public especies Borrar(especies e) => aplicacion.Borrar(e);
}
