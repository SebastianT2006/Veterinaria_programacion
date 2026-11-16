using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class clientesController : ControllerBase
{
    private readonly IclientesAplicacion aplicacion; public clientesController(IclientesAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<clientes> Consultar() => aplicacion.Consultar(); [HttpPost] public List<clientes> Buscar(clientes e) => aplicacion.Buscar(e); [HttpPost] public clientes Insertar(clientes e) => aplicacion.Insert(e); [HttpPost] public clientes Actualizar(clientes e) => aplicacion.Actualizar(e); [HttpPost] public clientes Borrar(clientes e) => aplicacion.Borrar(e);
}
