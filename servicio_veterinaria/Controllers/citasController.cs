using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class citasController : ControllerBase
{
    private readonly IcitasAplicacion aplicacion; public citasController(IcitasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<citas> Consultar() => aplicacion.Consultar(); [HttpPost] public List<citas> Buscar(citas e) => aplicacion.Buscar(e); [HttpPost] public citas Insertar(citas e) => aplicacion.Insert(e); [HttpPost] public citas Actualizar(citas e) => aplicacion.Actualizar(e); [HttpPost] public citas Borrar(citas e) => aplicacion.Borrar(e);
}
