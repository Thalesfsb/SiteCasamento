using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Services;

public class RSVPModel : PageModel
{
    private readonly AppDbContext _db;

    public RSVPModel(AppDbContext db) => _db = db;

    [BindProperty]
    public string NomeConvite { get; set; } = "";

    [BindProperty]
    public string Ultimos4Telefone { get; set; } = "";

    public string? Erro { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        //var nome = TextService.Normalizar(NomeConvite);
        var tel = TextService.SomenteDigitos(Ultimos4Telefone);

        if (NomeConvite.Length < 3 || tel.Length != 4)
        {
            Erro = "Informe um nome válido e os 4 últimos dígitos do telefone.";
            return Page();
        }

        // Segurança simples: não retornamos lista de convites, só tentamos achar 1 que bata
        var convite = _db.Convites
            .FirstOrDefault(c => c.NomeNormalizado.Contains(NomeConvite) || c.TelefoneUltimos4 == tel);

        if (convite is null)
        {
            // Mensagem propositalmente genérica (anti-enumeração)
            Erro = "Convite não encontrado ou dados inválidos.";
            return Page();
        }

        // Achou e validou: vai para a página de “Quem irá?”
        return RedirectToPage("/RSVPConfirmar", new { id = convite.Id });
    }
}