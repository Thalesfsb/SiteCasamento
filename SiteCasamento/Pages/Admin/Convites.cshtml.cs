using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class ConvitesModel : PageModel
{
    private readonly AppDbContext _db;
    public ConvitesModel(AppDbContext db) => _db = db;

    public List<Convite> Convites { get; set; } = new();

    public void OnGet()
    {
        Convites = _db.Convites
            .AsNoTracking()
            .Include(c => c.Pessoas)
            .OrderBy(c => c.NomeExibicao)
            .ToList();
    }
}