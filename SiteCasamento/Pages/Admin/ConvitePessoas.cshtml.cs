using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class ConvitePessoasModel : PageModel
{
    private readonly AppDbContext _db;
    public ConvitePessoasModel(AppDbContext db) => _db = db;

    public Convite? Convite { get; set; }

    public IActionResult OnGet(int id)
    {
        Convite = _db.Convites
            .Include(c => c.Pessoas)
            .FirstOrDefault(c => c.Id == id);

        return Convite is null ? NotFound() : Page();
    }
}