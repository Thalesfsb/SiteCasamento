using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;

namespace SiteCasamento.Pages.Admin;

public class ConfirmadosModel : PageModel
{
    private readonly AppDbContext _db;
    public ConfirmadosModel(AppDbContext db) => _db = db;

    public List<Item> Itens { get; set; } = new();

    public class Item
    {
        public string Nome { get; set; } = "";
        public string Convite { get; set; } = "";
        public DateTime? DataResposta { get; set; }
    }

    public void OnGet()
    {
        Itens = _db.PessoasConvite
            .AsNoTracking()
            .Where(p => p.Vai == true)
            .Include(p => p.Convite)
            .OrderBy(p => p.Nome)
            .Select(p => new Item
            {
                Nome = p.Nome,
                Convite = p.Convite.NomeExibicao,
                DataResposta = p.DataResposta
            })
            .ToList();
    }
}