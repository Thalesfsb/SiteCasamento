using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PresenteDeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public PresenteDeleteModel(AppDbContext db) => _db = db;

    public Presente? Presente { get; set; }

    public IActionResult OnGet(int id)
    {
        Presente = _db.Presentes.FirstOrDefault(x => x.Id == id);
        return Presente is null ? NotFound() : Page();
    }

    public IActionResult OnPost(int id)
    {
        var p = _db.Presentes.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound();

        _db.Presentes.Remove(p);
        _db.SaveChanges();

        return RedirectToPage("/Admin/Presentes");
    }
}