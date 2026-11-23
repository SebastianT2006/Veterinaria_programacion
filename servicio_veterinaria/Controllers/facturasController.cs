using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class facturasController : ControllerBase
{
    private readonly IfacturasAplicacion aplicacion; public facturasController(IfacturasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<facturas> Consultar() => aplicacion.Consultar(); [HttpPost] public List<facturas> Buscar(facturas e) => aplicacion.Buscar(e); [HttpPost] public facturas Insertar(facturas e) => aplicacion.Insert(e); [HttpPost] public facturas Actualizar(facturas e) => aplicacion.Actualizar(e); [HttpPost] public facturas Borrar(facturas e) => aplicacion.Borrar(e);
}
