using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PresenteCreateModel : PageModel
{
    private readonly AppDbContext _db;

    public PresenteCreateModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Presente Presente { get; set; } = new();

    public void OnGet()
    {
        Presente.Status = StatusPresente.Disponivel;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        _db.Presentes.Add(Presente);
        _db.SaveChanges();

        return RedirectToPage("/Admin/Presentes");
    }
}