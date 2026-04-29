using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PresentesModel : PageModel
{
    private readonly AppDbContext _db;

    public PresentesModel(AppDbContext db)
    {
        _db = db;
    }

    public List<Item> Itens { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public StatusPresente? Status { get; set; }

    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public decimal Valor { get; set; }

        public StatusPresente Status { get; set; }
        public string? ReservadoPor { get; set; }
    }

    public void OnGet()
    {
        var query = _db.Presentes.AsNoTracking();

        if (Status.HasValue)
            query = query.Where(p => p.Status == Status.Value);

        Itens = query
            .OrderBy(p => p.Nome)
            .Select(p => new Item
            {
                Id = p.Id,
                Nome = p.Nome,
                Valor = p.Valor,
                Status = p.Status,
                ReservadoPor = p.ReservadoPor
            }).ToList();
    }
}