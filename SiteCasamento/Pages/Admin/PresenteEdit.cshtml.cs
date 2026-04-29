using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PresenteEditModel : PageModel
{
    private readonly AppDbContext _db;
    public PresenteEditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Presente Presente { get; set; } = default!;

    public IActionResult OnGet(int id)
    {
        var p = _db.Presentes.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound();

        Presente = p;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        _db.Presentes.Update(Presente);
        _db.SaveChanges();

        return RedirectToPage("/Admin/Presentes");
    }
}