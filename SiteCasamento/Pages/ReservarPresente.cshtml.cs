using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;

public class ReservarPresenteModel : PageModel
{
    private readonly AppDbContext _db;

    public ReservarPresenteModel(AppDbContext db)
    {
        _db = db;
    }

    public Presente? Presente { get; set; }

    [BindProperty]
    public string NomeConvidado { get; set; } = string.Empty;

    public string? MensagemErro { get; set; }
    public bool Sucesso { get; set; }

    public IActionResult OnGet(int id)
    {
        Presente = _db.Presentes
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);

        return Presente is null ? NotFound() : Page();
    }

    public IActionResult OnPost(int id)
    {
        if (string.IsNullOrWhiteSpace(NomeConvidado))
        {
            MensagemErro = "Por favor, informe seu nome para reservar.";
            Presente = _db.Presentes.AsNoTracking().FirstOrDefault(p => p.Id == id);
            return Page();
        }

        var nome = NomeConvidado.Trim();
        var agora = DateTime.Now;

        var linhasAfetadas = _db.Presentes
            .Where(p => p.Id == id && p.Status == StatusPresente.Disponivel)
            .ExecuteUpdate(setters => setters
                .SetProperty(p => p.Status, StatusPresente.Reservado)
                .SetProperty(p => p.ReservadoPor, nome)
                .SetProperty(p => p.DataReserva, agora)
            );

        if (linhasAfetadas == 0)
        {
            MensagemErro = "Ops! Esse presente não está mais disponível. Escolha outro.";
            Presente = _db.Presentes.AsNoTracking().FirstOrDefault(p => p.Id == id);
            return Page();
        }

        Presente = _db.Presentes.AsNoTracking().FirstOrDefault(p => p.Id == id);
        Sucesso = true;

        return Page();
    }
}