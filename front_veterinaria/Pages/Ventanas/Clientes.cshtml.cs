using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class ClientesModel : PageModel
{
    private readonly IclientesPresentacion presentacion;
    private readonly IpersonasPresentacion personasPresentacion;
    public ClientesModel(IclientesPresentacion p, IpersonasPresentacion pers)
    {
        presentacion = p;
        personasPresentacion = pers;
        Filtro = new clientes();
    }
    [BindProperty] public List<clientes> Lista { get; set; } = new(); [BindProperty]
    public clientes? Filtro
    {
        get; set;
    }
    [BindProperty]
    public clientes? Actual
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
            Lista = presentacion.Buscar(Filtro ?? new clientes());
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtNuevo()
    {
        OnPostBtConsultar();
        Formulario = true;
        Actual = new clientes();
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

