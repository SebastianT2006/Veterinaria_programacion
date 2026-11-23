using lib_aplicaciones.entidades;
using lib_aplicaciones.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_veterinaria.interfaces;

namespace front_veterinaria.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IusuariosPresentacion presentacion;

        public IndexModel(IusuariosPresentacion presentacion)
        {
            this.presentacion = presentacion;
        }

        [BindProperty] public credenciales Datos { get; set; } = new();
        public string? Usuario { get; set; }
        public string? Rol { get; set; }

        public void OnGet()
        {
            CargarSesion();
        }

        public void OnPostBtIngresar()
        {
            try
            {
                var sesion = presentacion.Login(Datos);
                // La sesion guarda quien entro: las demas paginas la consultan (FiltroSesion y menu)
                HttpContext.Session.SetString("Usuario", sesion.NombreUsuario!);
                HttpContext.Session.SetString("UsuarioId", sesion.Id.ToString());
                HttpContext.Session.SetString("Rol", sesion.Rol!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData);
            }
            CargarSesion();
        }

        // Boton "Cerrar" de la ventana de mensajes
        public void OnPostBtConsultar()
        {
            CargarSesion();
        }

        private void CargarSesion()
        {
            Usuario = HttpContext.Session.GetString("Usuario");
            Rol = HttpContext.Session.GetString("Rol");
        }
    }
}
