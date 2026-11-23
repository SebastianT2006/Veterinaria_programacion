using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class historiasClinicasController : ControllerBase
{
    private readonly IhistoriasClinicasAplicacion aplicacion; public historiasClinicasController(IhistoriasClinicasAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }
    [HttpPost] public List<historiasClinicas> Consultar() => aplicacion.Consultar(); [HttpPost] public List<historiasClinicas> Buscar(historiasClinicas e) => aplicacion.Buscar(e); [HttpPost] public historiasClinicas Insertar(historiasClinicas e) => aplicacion.Insert(e); [HttpPost] public historiasClinicas Actualizar(historiasClinicas e) => aplicacion.Actualizar(e); [HttpPost] public historiasClinicas Borrar(historiasClinicas e) => aplicacion.Borrar(e);
}
