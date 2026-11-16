using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class detalleFacturasController : ControllerBase
{
    private readonly IdetalleFacturasAplicacion aplicacion; public detalleFacturasController(IdetalleFacturasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<detalleFacturas> Consultar() => aplicacion.Consultar(); [HttpPost] public List<detalleFacturas> Buscar(detalleFacturas e) => aplicacion.Buscar(e); [HttpPost] public detalleFacturas Insertar(detalleFacturas e) => aplicacion.Insert(e); [HttpPost] public detalleFacturas Actualizar(detalleFacturas e) => aplicacion.Actualizar(e); [HttpPost] public detalleFacturas Borrar(detalleFacturas e) => aplicacion.Borrar(e);
}
