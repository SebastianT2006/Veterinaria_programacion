using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class EspeciesModel : PageModel
{
    private readonly IespeciesPresentacion presentacion; public EspeciesModel(IespeciesPresentacion p)
    {
        presentacion = p;
        Filtro = new especies();
    }
    [BindProperty] public List<especies> Lista { get; set; } = new(); [BindProperty]
    public especies? Filtro
    {
        get; set;
    }
    [BindProperty]
    public especies? Actual
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
            Lista = presentacion.Buscar(Filtro ?? new especies());
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtNuevo()
    {
        Formulario = true;
        Actual = new especies();
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
}

