using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class rolesController : ControllerBase
{
    private readonly IrolesAplicacion aplicacion;

    public rolesController(IrolesAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }

    [HttpPost]
    public List<roles> Consultar() => aplicacion.Consultar();

    [HttpPost]
    public List<roles> Buscar(roles e) => aplicacion.Buscar(e);

    [HttpPost]
    public roles Insertar(roles e) => aplicacion.Insert(e);

    [HttpPost]
    public roles Actualizar(roles e) => aplicacion.Actualizar(e);

    [HttpPost]
    public roles Borrar(roles e) => aplicacion.Borrar(e);
}
