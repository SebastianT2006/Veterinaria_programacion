using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class EmpleadosModel : PageModel
{
    private readonly IempleadosPresentacion presentacion;
    private readonly IpersonasPresentacion personasPresentacion;
    public EmpleadosModel(IempleadosPresentacion p, IpersonasPresentacion pers)
    {
        presentacion = p;
        personasPresentacion = pers;
        Filtro = new empleados();
    }
    [BindProperty] public List<empleados> Lista { get; set; } = new(); [BindProperty]
    public empleados? Filtro
    {
        get; set;
    }
    [BindProperty]
    public empleados? Actual
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
    public List<personas> Personas { get; set; } = new();
    public void OnGet() => OnPostBtConsultar(); public void OnPostBtConsultar()
    {
        try
        {
            Personas = personasPresentacion.Consultar();
            Lista = presentacion.Buscar(Filtro ?? new empleados());
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtNuevo()
    {
        OnPostBtConsultar();
        Formulario = true;
        Actual = new empleados();
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

