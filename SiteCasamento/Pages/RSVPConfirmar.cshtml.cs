using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;

public class RSVPConfirmarModel : PageModel
{
    private readonly AppDbContext _db;
    public RSVPConfirmarModel(AppDbContext db) => _db = db;

    public Convite? Convite { get; set; }

    [BindProperty]
    public string? Mensagem { get; set; }

    [BindProperty]
    public List<PessoaRespostaInput> Respostas { get; set; } = new();

    public bool Enviado { get; set; }

    public class PessoaRespostaInput
    {
        public int PessoaId { get; set; }
        public bool? Vai { get; set; }
    }

    public IActionResult OnGet(int id)
    {
        Convite = _db.Convites
            .Include(c => c.Pessoas)
            .FirstOrDefault(c => c.Id == id);

        if (Convite is null) return NotFound();

        // Prepara lista para o form
        Respostas = Convite.Pessoas
            .OrderBy(p => p.Id)
            .Select(p => new PessoaRespostaInput
            {
                PessoaId = p.Id,
                Vai = p.Vai
            }).ToList();

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        var convite = _db.Convites
            .Include(c => c.Pessoas)
            .FirstOrDefault(c => c.Id == id);

        if (convite is null) return NotFound();


        // Validação: obriga escolher Sim ou Não para todos
        if (Respostas.Any(r => r.Vai is null))
        {
            ModelState.AddModelError(string.Empty, "Por favor, marque Sim ou Não para todas as pessoas.");
            Convite = convite; // precisa para re-renderizar os nomes na tela
            return Page();
        }

        // Atualiza respostas
        var agora = DateTime.Now;

        foreach (var r in Respostas)
        {
            var pessoa = convite.Pessoas.FirstOrDefault(p => p.Id == r.PessoaId);
            if (pessoa is null) continue;

            pessoa.Vai = r.Vai!.Value;
            pessoa.DataResposta = agora;
        }

        // MARCA O CONVITE COMO RESPONDIDO
        convite.DataUltimaResposta = agora;

        // Mensagem opcional
        convite.Mensagem = Mensagem?.Trim();

        _db.SaveChanges();

        Enviado = true;
        Convite = convite;
        return Page();
    }
}