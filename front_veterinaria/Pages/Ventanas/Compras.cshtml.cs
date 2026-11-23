using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class ComprasModel : PageModel
{
    private readonly IcomprasPresentacion presentacion;
    private readonly IproveedoresPresentacion proveedoresPresentacion;
    public ComprasModel(IcomprasPresentacion p, IproveedoresPresentacion prov)
    {
        presentacion = p;
        proveedoresPresentacion = prov;
        Filtro = new compras();
    }
    [BindProperty] public List<compras> Lista { get; set; } = new(); [BindProperty]
    public compras? Filtro
    {
        get; set;
    }
    [BindProperty]
    public compras? Actual
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
    public List<proveedores> Proveedores { get; set; } = new();
    public void OnGet() => OnPostBtConsultar(); public void OnPostBtConsultar()
    {
        try
        {
            Proveedores = proveedoresPresentacion.Consultar();
            Lista = presentacion.Buscar(Filtro ?? new compras());
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtNuevo()
    {
        OnPostBtConsultar();
        Formulario = true;
        Actual = new compras();
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

