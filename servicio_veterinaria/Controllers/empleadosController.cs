using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class empleadosController : ControllerBase
{
    private readonly IempleadosAplicacion aplicacion; public empleadosController(IempleadosAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<empleados> Consultar() => aplicacion.Consultar(); [HttpPost] public List<empleados> Buscar(empleados e) => aplicacion.Buscar(e); [HttpPost] public empleados Insertar(empleados e) => aplicacion.Insert(e); [HttpPost] public empleados Actualizar(empleados e) => aplicacion.Actualizar(e); [HttpPost] public empleados Borrar(empleados e) => aplicacion.Borrar(e);
}
