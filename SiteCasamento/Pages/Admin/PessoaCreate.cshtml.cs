using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PessoaCreateModel : PageModel
{
    private readonly AppDbContext _db;
    public PessoaCreateModel(AppDbContext db) => _db = db;

    [BindProperty]
    public PessoaConvite Pessoa { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int ConviteId { get; set; }

    public IActionResult OnGet(int conviteId)
    {
        ConviteId = conviteId;

        if (!_db.Convites.Any(c => c.Id == ConviteId))
            return NotFound();

        Pessoa.ConviteId = ConviteId;

        return Page();
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Pessoa.Nome))
            ModelState.AddModelError("", "Nome é obrigatório.");

        if (!ModelState.IsValid) return Page();

        _db.PessoasConvite.Add(Pessoa);
        _db.SaveChanges();

        return RedirectToPage("/Admin/ConvitePessoas", new { id = Pessoa.ConviteId });
    }
}