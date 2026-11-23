using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class detalleComprasController : ControllerBase
{
    private readonly IdetalleComprasAplicacion aplicacion; public detalleComprasController(IdetalleComprasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<detalleCompras> Consultar() => aplicacion.Consultar(); [HttpPost] public List<detalleCompras> Buscar(detalleCompras e) => aplicacion.Buscar(e); [HttpPost] public detalleCompras Insertar(detalleCompras e) => aplicacion.Insert(e); [HttpPost] public detalleCompras Actualizar(detalleCompras e) => aplicacion.Actualizar(e); [HttpPost] public detalleCompras Borrar(detalleCompras e) => aplicacion.Borrar(e);
}
