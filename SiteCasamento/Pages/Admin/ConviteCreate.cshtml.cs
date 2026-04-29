using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;
using SiteCasamento.Services;

namespace SiteCasamento.Pages.Admin;

public class ConviteCreateModel : PageModel
{
    private readonly AppDbContext _db;
    public ConviteCreateModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Convite Convite { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        // validações simples
        if (string.IsNullOrWhiteSpace(Convite.NomeExibicao))
            ModelState.AddModelError("", "Nome do convite é obrigatório.");

        var tel = TextService.SomenteDigitos(Convite.TelefoneUltimos4);
        if (tel.Length != 4)
            ModelState.AddModelError("", "Telefone deve ter exatamente 4 dígitos.");

        if (!ModelState.IsValid) return Page();

        Convite.TelefoneUltimos4 = tel;
        Convite.NomeNormalizado = TextService.Normalizar(Convite.NomeExibicao);

        _db.Convites.Add(Convite);
        _db.SaveChanges();

        return RedirectToPage("/Admin/Convites");
    }
}