using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class animalesController : ControllerBase
{
    private readonly IanimalesAplicacion aplicacion; public animalesController(IanimalesAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<animales> Consultar() => aplicacion.Consultar(); [HttpPost] public List<animales> Buscar(animales e) => aplicacion.Buscar(e); [HttpPost] public animales Insertar(animales e) => aplicacion.Insert(e); [HttpPost] public animales Actualizar(animales e) => aplicacion.Actualizar(e); [HttpPost] public animales Borrar(animales e) => aplicacion.Borrar(e);
}
