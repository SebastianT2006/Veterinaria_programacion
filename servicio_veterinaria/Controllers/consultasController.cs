using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class consultasController : ControllerBase
{
    private readonly IconsultasAplicacion aplicacion; public consultasController(IconsultasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<consultas> Consultar() => aplicacion.Consultar(); [HttpPost] public List<consultas> Buscar(consultas e) => aplicacion.Buscar(e); [HttpPost] public consultas Insertar(consultas e) => aplicacion.Insert(e); [HttpPost] public consultas Actualizar(consultas e) => aplicacion.Actualizar(e); [HttpPost] public consultas Borrar(consultas e) => aplicacion.Borrar(e);
}
