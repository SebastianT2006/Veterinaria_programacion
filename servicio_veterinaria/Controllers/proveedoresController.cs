using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class proveedoresController : ControllerBase
{
    private readonly IproveedoresAplicacion aplicacion; public proveedoresController(IproveedoresAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<proveedores> Consultar() => aplicacion.Consultar(); [HttpPost] public List<proveedores> Buscar(proveedores e) => aplicacion.Buscar(e); [HttpPost] public proveedores Insertar(proveedores e) => aplicacion.Insert(e); [HttpPost] public proveedores Actualizar(proveedores e) => aplicacion.Actualizar(e); [HttpPost] public proveedores Borrar(proveedores e) => aplicacion.Borrar(e);
}
