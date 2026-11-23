using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace front_veterinaria.Pages;

public class SalirModel : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Index");
    }
}
