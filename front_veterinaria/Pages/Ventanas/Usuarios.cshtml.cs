using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;
namespace front_veterinaria.Pages.Ventanas;

public class UsuariosModel : PageModel
{
    private readonly IusuariosPresentacion presentacion;
    private readonly IrolesPresentacion rolesPresentacion;

    public UsuariosModel(IusuariosPresentacion p, IrolesPresentacion r)
    {
        presentacion = p;
        rolesPresentacion = r;
        Filtro = new usuarios();
    }

    [BindProperty] public List<usuarios> Lista { get; set; } = new();
    [BindProperty] public usuarios? Filtro { get; set; }
    [BindProperty] public usuarios? Actual { get; set; }
    [BindProperty] public bool Formulario { get; set; }
    [BindProperty] public bool Borrar { get; set; }

    // Lista de roles para la lista desplegable y para mostrar el nombre del rol
    public List<roles> Roles { get; set; } = new();

    public void OnGet() => OnPostBtConsultar();

    public void OnPostBtConsultar()
    {
        try
        {
            Roles = rolesPresentacion.Consultar();
            Lista = presentacion.Buscar(Filtro ?? new usuarios());
        }
        catch (Exception ex)
        {
            LogConversor.Log(ex, ViewData);
        }
    }

    public void OnPostBtNuevo()
    {
        try
        {
            Roles = rolesPresentacion.Consultar();
        }
        catch (Exception ex)
        {
            LogConversor.Log(ex, ViewData);
        }
        Formulario = true;
        Actual = new usuarios { Estado = "Activo" };
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
