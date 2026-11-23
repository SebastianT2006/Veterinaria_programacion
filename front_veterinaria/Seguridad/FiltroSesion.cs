using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace front_veterinaria.Seguridad;

// Se ejecuta antes de abrir cualquier pagina: revisa que haya sesion y que el rol pueda entrar.
public class FiltroSesion : IPageFilter
{
    private static readonly string[] PaginasLibres = { "/Index", "/Error", "/Privacy", "/Salir" };

    // Ventanas del rol "Usuario". El Administrador entra a todas.
    private static readonly string[] VentanasDeUsuario =
    {
        "/Ventanas/Personas", "/Ventanas/Clientes", "/Ventanas/Fincas", "/Ventanas/Animales", "/Ventanas/Especies", "/Ventanas/Razas", "/Ventanas/Citas", "/Ventanas/Consultas", "/Ventanas/HistoriasClinicas", "/Ventanas/Vacunaciones", "/Ventanas/Tratamientos"
    };

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var pagina = context.ActionDescriptor.ViewEnginePath;
        if (PaginasLibres.Contains(pagina))
            return;

        var sesion = context.HttpContext.Session;
        if (string.IsNullOrEmpty(sesion.GetString("Usuario")))
        {
            context.Result = new RedirectToPageResult("/Index");
            return;
        }

        if (sesion.GetString("Rol") != "Administrador" && !VentanasDeUsuario.Contains(pagina))
            context.Result = new RedirectToPageResult("/Index");
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    }
}
