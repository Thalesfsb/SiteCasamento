using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class DashboardModel : PageModel
{
    private readonly AppDbContext _db;

    public DashboardModel(AppDbContext db) => _db = db;

    // ===== RSVP =====
    public int TotalConvites { get; set; }
    public int TotalPessoas { get; set; }
    public int TotalConfirmados { get; set; }
    public int TotalRecusas { get; set; }
    public int TotalPendentes { get; set; }
    public int ConvitesRespondidos { get; set; }

    // ===== Presentes =====
    public int PresentesDisponiveis { get; set; }
    public int PresentesReservados { get; set; }
    public int PresentesPresenteados { get; set; }

    // ===== Pagamentos =====
    public int PagamentosPendentes { get; set; }

    public void OnGet()
    {
        // ===== RSVP =====
        TotalConvites = _db.Convites.Count();
        TotalPessoas = _db.PessoasConvite.Count();

        TotalConfirmados = _db.PessoasConvite.Count(p => p.Vai == true);
        TotalRecusas = _db.PessoasConvite.Count(p => p.Vai == false);
        TotalPendentes = _db.PessoasConvite.Count(p => p.Vai == null);

        ConvitesRespondidos = _db.Convites.Count(c => c.DataUltimaResposta != null);

        // ===== Presentes =====
        PresentesDisponiveis = _db.Presentes.Count(p => p.Status == StatusPresente.Disponivel);
        PresentesReservados = _db.Presentes.Count(p => p.Status == StatusPresente.Reservado);
        PresentesPresenteados = _db.Presentes.Count(p => p.Status == StatusPresente.Presenteado);

        // ===== Pagamentos =====
        PagamentosPendentes = _db.PagamentosPix.Count(p => !p.Confirmado);
    }
}