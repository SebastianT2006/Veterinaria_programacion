using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class RolesModel : PageModel
{
    private readonly IrolesPresentacion presentacion;

    public RolesModel(IrolesPresentacion p)
    {
        presentacion = p;
        Filtro = new roles();
    }

    [BindProperty] public List<roles> Lista { get; set; } = new();
    [BindProperty] public roles? Filtro { get; set; }
    [BindProperty] public roles? Actual { get; set; }
    [BindProperty] public bool Formulario { get; set; }
    [BindProperty] public bool Borrar { get; set; }

    public void OnGet() => OnPostBtConsultar();

    public void OnPostBtConsultar()
    {
        try
        {
            Lista = presentacion.Buscar(Filtro ?? new roles());
        }
        catch (Exception ex)
        {
            LogConversor.Log(ex, ViewData);
        }
    }

    public void OnPostBtNuevo()
    {
        Formulario = true;
        Actual = new roles();
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
        catch (Exception ex)
        {
            LogConversor.Log(ex, ViewData);
        }
        finally
        {
            OnPostBtConsultar();
        }
    }

    public void OnPostBtEditar(string data)
    {
        try
        {
            Formulario = true;
            OnPostBtConsultar();
            Actual = Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
        }
        catch (Exception ex)
        {
            LogConversor.Log(ex, ViewData);
        }
    }

    public void OnPostBtPregBorrar(string data)
    {
        try
        {
            Borrar = true;
            OnPostBtConsultar();
            Actual = Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
        }
        catch (Exception ex)
        {
            LogConversor.Log(ex, ViewData);
        }
    }

    public void OnPostBtBorrar()
    {
        try
        {
            if (Actual!.Id != 0)
                presentacion.Borrar(Actual);
        }
        catch (Exception ex)
        {
            LogConversor.Log(ex, ViewData);
        }
        finally
        {
            OnPostBtConsultar();
        }
    }
}
