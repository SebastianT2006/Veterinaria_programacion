using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class auditoriasController : ControllerBase
{
    private readonly IauditoriasAplicacion aplicacion;

    public auditoriasController(IauditoriasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }

    [HttpPost]
    public List<auditorias> Consultar() => aplicacion.Consultar();

    [HttpPost]
    public List<auditorias> Buscar(auditorias e) => aplicacion.Buscar(e);
}
