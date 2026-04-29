using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PessoaDeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public PessoaDeleteModel(AppDbContext db) => _db = db;

    public PessoaConvite? Pessoa { get; set; }

    public IActionResult OnGet(int id)
    {
        Pessoa = _db.PessoasConvite.FirstOrDefault(x => x.Id == id);
        return Pessoa is null ? NotFound() : Page();
    }

    public IActionResult OnPost(int id)
    {
        var p = _db.PessoasConvite.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound();

        var conviteId = p.ConviteId;
        _db.PessoasConvite.Remove(p);
        _db.SaveChanges();

        return RedirectToPage("/Admin/ConvitePessoas", new { id = conviteId });
    }
}