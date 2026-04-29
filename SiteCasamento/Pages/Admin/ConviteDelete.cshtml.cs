using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class ConviteDeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public ConviteDeleteModel(AppDbContext db) => _db = db;

    public Convite? Convite { get; set; }

    public IActionResult OnGet(int id)
    {
        Convite = _db.Convites.FirstOrDefault(x => x.Id == id);
        return Convite is null ? NotFound() : Page();
    }

    public IActionResult OnPost(int id)
    {
        var c = _db.Convites.FirstOrDefault(x => x.Id == id);
        if (c is null) return NotFound();

        _db.Convites.Remove(c);
        _db.SaveChanges();

        return RedirectToPage("/Admin/Convites");
    }
}