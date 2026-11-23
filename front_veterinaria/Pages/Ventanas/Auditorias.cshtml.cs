using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

// Ventana de solo lectura: muestra quien hizo que y cuando
public class AuditoriasModel : PageModel
{
    private readonly IauditoriasPresentacion presentacion;
    private readonly IusuariosPresentacion usuariosPresentacion;

    public AuditoriasModel(IauditoriasPresentacion p, IusuariosPresentacion u)
    {
        presentacion = p;
        usuariosPresentacion = u;
        Filtro = new auditorias();
    }

    [BindProperty] public List<auditorias> Lista { get; set; } = new();
    [BindProperty] public auditorias? Filtro { get; set; }

    public List<usuarios> Usuarios { get; set; } = new();

    public void OnGet() => OnPostBtConsultar();

    public void OnPostBtConsultar()
    {
        try
        {
            Usuarios = usuariosPresentacion.Consultar();
            Lista = presentacion.Buscar(Filtro ?? new auditorias());
        }
        catch (Exception ex)
        {
            LogConversor.Log(ex, ViewData);
        }
    }

    public string NombreDe(int? id)
    {
        if (id == null)
            return "(sin usuario)";
        return Usuarios.FirstOrDefault(u => u.Id == id)?.NombreUsuario ?? id.ToString()!;
    }
}
