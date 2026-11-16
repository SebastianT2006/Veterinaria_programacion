using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class comprasController : ControllerBase
{
    private readonly IcomprasAplicacion aplicacion; public comprasController(IcomprasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<compras> Consultar() => aplicacion.Consultar(); [HttpPost] public List<compras> Buscar(compras e) => aplicacion.Buscar(e); [HttpPost] public compras Insertar(compras e) => aplicacion.Insert(e); [HttpPost] public compras Actualizar(compras e) => aplicacion.Actualizar(e); [HttpPost] public compras Borrar(compras e) => aplicacion.Borrar(e);
}
