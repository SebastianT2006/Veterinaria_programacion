using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class productosController : ControllerBase
{
    private readonly IproductosAplicacion aplicacion; public productosController(IproductosAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<productos> Consultar() => aplicacion.Consultar(); [HttpPost] public List<productos> Buscar(productos e) => aplicacion.Buscar(e); [HttpPost] public productos Insertar(productos e) => aplicacion.Insert(e); [HttpPost] public productos Actualizar(productos e) => aplicacion.Actualizar(e); [HttpPost] public productos Borrar(productos e) => aplicacion.Borrar(e);
}
