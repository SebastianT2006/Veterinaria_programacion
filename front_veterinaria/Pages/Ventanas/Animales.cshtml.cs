using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using front_veterinaria.Reportes;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class AnimalesModel : PageModel
{
    private readonly IanimalesPresentacion presentacion;
    private readonly IrazasPresentacion razasPresentacion;
    private readonly IfincasPresentacion fincasPresentacion;
    public AnimalesModel(IanimalesPresentacion p, IrazasPresentacion r, IfincasPresentacion f)
    {
        presentacion = p;
        razasPresentacion = r;
        fincasPresentacion = f;
        Filtro = new animales();
    }
    [BindProperty] public List<animales> Lista { get; set; } = new(); [BindProperty]
    public animales? Filtro
    {
        get; set;
    }
    [BindProperty]
    public animales? Actual
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
    public List<razas> Razas { get; set; } = new();
    public List<fincas> Fincas { get; set; } = new();
    public void OnGet() => OnPostBtConsultar(); public void OnPostBtConsultar()
    {
        try
        {
            Razas = razasPresentacion.Consultar();
            Fincas = fincasPresentacion.Consultar();
            Lista = presentacion.Buscar(Filtro ?? new animales());
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtNuevo()
    {
        OnPostBtConsultar();
        Formulario = true;
        Actual = new animales();
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
            Razas.FirstOrDefault(r => r.Id == x.Raza)?.Nombre ?? "",
            x.Sexo ?? "",
            x.Color ?? "",
            x.Peso.ToString(),
            x.Estado ?? ""
        }).ToList();
        var columnas = new[] { "Nombre", "Raza", "Sexo", "Color", "Peso", "Estado" };
        return File(ReportePdf.Generar("Animales", columnas, filas), "application/pdf", "Animales.pdf");
    }
}
