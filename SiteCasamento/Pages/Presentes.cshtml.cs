using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;
using SiteCasamento.Services;

public class PresentesModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ReservaService _reservaService;
    public PresentesModel(AppDbContext context, ReservaService reservaService)
    {
        _context = context;
        _reservaService = reservaService;
    }

    public List<Presente> Presentes { get; set; } = new();

    public void OnGet()
    {
        _reservaService.LiberarReservasExpiradas();

        Presentes = _context.Presentes
            .AsNoTracking()
            .OrderBy(p => p.Status)
            .ThenBy(p => p.Nome)
            .ToList();
    }
}
