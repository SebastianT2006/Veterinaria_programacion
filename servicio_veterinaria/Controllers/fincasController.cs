using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class fincasController : ControllerBase
{
    private readonly IfincasAplicacion aplicacion; public fincasController(IfincasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<fincas> Consultar() => aplicacion.Consultar(); [HttpPost] public List<fincas> Buscar(fincas e) => aplicacion.Buscar(e); [HttpPost] public fincas Insertar(fincas e) => aplicacion.Insert(e); [HttpPost] public fincas Actualizar(fincas e) => aplicacion.Actualizar(e); [HttpPost] public fincas Borrar(fincas e) => aplicacion.Borrar(e);
}
