using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;

namespace SiteCasamento.Pages.Admin;

public class RSVPModel : PageModel
{
    private readonly AppDbContext _db;

    public RSVPModel(AppDbContext db)
    {
        _db = db;
    }

    public List<Item> Itens { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Filtro { get; set; } // confirmado | recusado | pendente

    public int TotalConfirmados { get; set; }
    public int TotalRecusados { get; set; }
    public int TotalPendentes { get; set; }

    public class Item
    {
        public string Convite { get; set; } = "";
        public string Pessoa { get; set; } = "";
        public bool? Vai { get; set; }
    }

    public void OnGet()
    {
        // 1. Busca simples no banco (SEM SelectMany)
        var convites = _db.Convites
            .AsNoTracking()
            .Include(c => c.Pessoas)
            .ToList();

        // 2. Projeção em memória (C#)
        var pessoas = convites
            .SelectMany(c => c.Pessoas.Select(p => new
            {
                Convite = c.NomeExibicao,
                Pessoa = p.Nome,
                Vai = p.Vai
            }))
            .ToList();

        // 3. Totais
        TotalConfirmados = pessoas.Count(p => p.Vai == true);
        TotalRecusados = pessoas.Count(p => p.Vai == false);
        TotalPendentes = pessoas.Count(p => p.Vai == null);

        // 4. Filtro
        if (Filtro == "confirmado")
            pessoas = pessoas.Where(p => p.Vai == true).ToList();
        else if (Filtro == "recusado")
            pessoas = pessoas.Where(p => p.Vai == false).ToList();
        else if (Filtro == "pendente")
            pessoas = pessoas.Where(p => p.Vai == null).ToList();

        // 5. Binding final
        Itens = pessoas
            .OrderBy(p => p.Convite)
            .ThenBy(p => p.Pessoa)
            .Select(p => new Item
            {
                Convite = p.Convite,
                Pessoa = p.Pessoa,
                Vai = p.Vai
            })
            .ToList();
    }
}