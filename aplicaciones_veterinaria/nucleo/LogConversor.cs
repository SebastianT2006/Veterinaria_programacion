
namespace lib_aplicaciones.nucleo
{
    public class LogConversor
    {
        public static void Log(Exception ex,
            IDictionary<string, object> ViewData)
        {
            if (ViewData == null)
                return;

            var interna = ex.GetBaseException();
            var mensaje = interna is ValidacionException ? interna.Message : "Error Comunicación";

            ViewData["Mensaje"] = mensaje;
        }
    }
}
