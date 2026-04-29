using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PessoaEditModel : PageModel
{
    private readonly AppDbContext _db;
    public PessoaEditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public PessoaConvite Pessoa { get; set; } = default!;

    public IActionResult OnGet(int id)
    {
        var p = _db.PessoasConvite.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound();
        Pessoa = p;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Pessoa.Nome))
            ModelState.AddModelError("", "Nome é obrigatório.");

        if (!ModelState.IsValid) return Page();

        _db.PessoasConvite.Update(Pessoa);
        _db.SaveChanges();

        return RedirectToPage("/Admin/ConvitePessoas", new { id = Pessoa.ConviteId });
    }
}