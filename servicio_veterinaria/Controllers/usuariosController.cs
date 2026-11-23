using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;
namespace servicio_veterinaria.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class usuariosController : ControllerBase
{
    private readonly IusuariosAplicacion aplicacion;

    public usuariosController(IusuariosAplicacion aplicacion)
    {
        this.aplicacion = aplicacion;
    }

    [HttpPost]
    public List<usuarios> Consultar() => aplicacion.Consultar();

    [HttpPost]
    public List<usuarios> Buscar(usuarios e) => aplicacion.Buscar(e);

    [HttpPost]
    public usuarios Insertar(usuarios e) => aplicacion.Insert(e);

    [HttpPost]
    public usuarios Actualizar(usuarios e) => aplicacion.Actualizar(e);

    [HttpPost]
    public usuarios Borrar(usuarios e) => aplicacion.Borrar(e);

    [HttpPost]
    public sesionUsuario Login(credenciales e) => aplicacion.Login(e);
}
