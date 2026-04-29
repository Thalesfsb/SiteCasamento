using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;
using SiteCasamento.Services;

namespace SiteCasamento.Pages.Admin;

public class ConviteEditModel : PageModel
{
    private readonly AppDbContext _db;
    public ConviteEditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Convite Convite { get; set; } = default!;

    public IActionResult OnGet(int id)
    {
        var c = _db.Convites.FirstOrDefault(x => x.Id == id);
        if (c is null) return NotFound();
        Convite = c;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Convite.NomeExibicao))
            ModelState.AddModelError("", "Nome do convite é obrigatório.");

        var tel = TextService.SomenteDigitos(Convite.TelefoneUltimos4);
        if (tel.Length != 4)
            ModelState.AddModelError("", "Telefone deve ter exatamente 4 dígitos.");

        if (!ModelState.IsValid) return Page();

        Convite.TelefoneUltimos4 = tel;
        Convite.NomeNormalizado = TextService.Normalizar(Convite.NomeExibicao);

        _db.Convites.Update(Convite);
        _db.SaveChanges();

        return RedirectToPage("/Admin/Convites");
    }
}