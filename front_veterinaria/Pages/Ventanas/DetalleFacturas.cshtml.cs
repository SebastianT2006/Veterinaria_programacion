using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class DetalleFacturasModel : PageModel
{
    private readonly IdetalleFacturasPresentacion presentacion;
    private readonly IproductosPresentacion productosPresentacion;
    private readonly IserviciosPresentacion serviciosPresentacion;
    public DetalleFacturasModel(IdetalleFacturasPresentacion p, IproductosPresentacion prod, IserviciosPresentacion serv)
    {
        presentacion = p;
        productosPresentacion = prod;
        serviciosPresentacion = serv;
        Filtro = new detalleFacturas();
    }
    [BindProperty] public List<detalleFacturas> Lista { get; set; } = new(); [BindProperty]
    public detalleFacturas? Filtro
    {
        get; set;
    }
    [BindProperty]
    public detalleFacturas? Actual
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
    public List<productos> Productos { get; set; } = new();
    public List<servicios> Servicios { get; set; } = new();
    public void OnGet() => OnPostBtConsultar(); public void OnPostBtConsultar()
    {
        try
        {
            Productos = productosPresentacion.Consultar();
            Servicios = serviciosPresentacion.Consultar();
            Lista = presentacion.Buscar(Filtro ?? new detalleFacturas());
        }
        catch (Exception ex) { LogConversor.Log(ex, ViewData); }
    }
    public void OnPostBtNuevo()
    {
        OnPostBtConsultar();
        Formulario = true;
        Actual = new detalleFacturas();
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

