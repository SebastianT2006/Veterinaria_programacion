using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using front_veterinaria.Reportes;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class PersonasModel : PageModel
{
    private readonly IpersonasPresentacion presentacion; public PersonasModel(IpersonasPresentacion p)
    {
        presentacion = p;
        Filtro = new personas();
    }
    [BindProperty] public List<personas> Lista { get; set; } = new(); [BindProperty]
    public personas? Filtro
    {
        get; set;
    }
    [BindProperty]
    public personas? Actual
    {
        get; set;
    }
    [BindProperty]
    public bool Formulario
    {
        get; set;
    }
    [BindProperty]
    public bool Borrar
    {
        get; set;
    }
    public void OnGet() => OnPostBtConsultar(); public void OnPostBtConsultar()
    {
        try
        {
            Lista = presentacion.Buscar(Filtro ?? new personas());
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtNuevo()
    {
        Formulario = true;
        Actual = new personas();
    }
    public void OnPostBtGuardar()
    {
        try
        {
            if (Actual!.Id == 0)
                Actual = presentacion.Insertar(Actual);
            else
                Actual = presentacion.Actualizar(Actual);
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
        finally { OnPostBtConsultar(); }
    }
    public void OnPostBtEditar(string data)
    {
        try
        {
            Formulario = true;
            OnPostBtConsultar();
            Actual = Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtPregBorrar(string data)
    {
        try
        {
            Borrar = true;
            OnPostBtConsultar();
            Actual = Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtBorrar()
    {
        try
        {
            if (Actual!.Id != 0)
                presentacion.Borrar(Actual);
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
        finally { OnPostBtConsultar(); }
    }

    // Boton PDF: exporta la lista tal como se ve (respeta la busqueda)
    public IActionResult OnPostBtPdf()
    {
        OnPostBtConsultar();
        var filas = Lista.Select(x => new[]
        {
            x.Nombre ?? "",
            x.Telefono ?? "",
            x.Email ?? "",
            x.Direccion ?? "",
            x.FechaNacimiento.ToString("dd/MM/yyyy")
        }).ToList();
        var columnas = new[] { "Nombre", "Teléfono", "Email", "Dirección", "Fecha de nacimiento" };
        return File(ReportePdf.Generar("Personas", columnas, filas), "application/pdf", "Personas.pdf");
    }
}
