using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using front_veterinaria.Reportes;
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

    // Boton PDF: exporta la lista tal como se ve (respeta la busqueda)
    public IActionResult OnPostBtPdf()
    {
        OnPostBtConsultar();
        var filas = Lista.Select(x => new[]
        {
            x.Fecha.ToString("dd/MM/yyyy HH:mm"),
            NombreDe(x.Usuario),
            x.Accion ?? "",
            x.Entidad ?? "",
            x.Registro?.ToString() ?? "",
            x.Detalle ?? ""
        }).ToList();
        var columnas = new[] { "Fecha", "Usuario", "Acción", "Entidad", "Registro", "Detalle" };
        return File(ReportePdf.Generar("Auditorías", columnas, filas), "application/pdf", "Auditorias.pdf");
    }
}
