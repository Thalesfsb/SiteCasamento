using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PresenteEditModel : PageModel
{
    private readonly AppDbContext _db;

    public PresenteEditModel(AppDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Presente Presente { get; set; } = default!;

    public IActionResult OnGet(int id)
    {
        Presente = _db.Presentes.FirstOrDefault(x => x.Id == id)!;

        if (Presente == null)
            return NotFound();

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(
                string.Empty,
                "Não foi possível salvar. Verifique os campos destacados abaixo."
            );

            return Page();
        }

        var entity = _db.Presentes.FirstOrDefault(x => x.Id == Presente.Id);

        if (entity == null)
            return NotFound();

        entity.Nome = Presente.Nome;

        entity.Descricao = Presente.Descricao ?? string.Empty;
        entity.ImagemUrl = Presente.ImagemUrl ?? string.Empty;

        entity.Valor = Presente.Valor;
        entity.Status = Presente.Status;
        entity.LinkCompra = Presente.LinkCompra;

        _db.SaveChanges();

        return RedirectToPage("/Admin/Presentes");
    }

}