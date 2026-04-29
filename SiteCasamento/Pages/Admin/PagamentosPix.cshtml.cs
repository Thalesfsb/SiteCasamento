using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Pages.Admin;

public class PagamentosPixModel : PageModel
{
    private readonly AppDbContext _db;

    public PagamentosPixModel(AppDbContext db)
    {
        _db = db;
    }

    public List<Item> Pendentes { get; set; } = new();

    public class Item
    {
        public int Id { get; set; }

        public int PresenteId { get; set; }
        public string PresenteNome { get; set; } = "";

        public string NomeConvidado { get; set; } = "";
        public TipoPagamento Tipo { get; set; }

        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }

        // Apenas para Compra Externa
        public string? Loja { get; set; }
        public string? NumeroPedido { get; set; }
        public string? Mensagem { get; set; }
    }

    public void OnGet()
    {
        Pendentes = _db.PagamentosPix
            .AsNoTracking()
            .Where(p => !p.Confirmado)
            .Include(p => p.Presente)
            .OrderByDescending(p => p.CriadoEm)
            .Select(p => new Item
            {
                Id = p.Id,
                PresenteId = p.PresenteId,
                PresenteNome = p.Presente.Nome,

                NomeConvidado = p.NomeConvidado,
                Tipo = p.Tipo,

                Valor = p.Presente.Valor,
                DataCriacao = p.CriadoEm,

                Loja = p.Loja,
                NumeroPedido = p.NumeroPedido,
                Mensagem = p.Mensagem
            })
            .ToList();
    }

    public IActionResult OnPostConfirmar(int id)
    {
        var pagamento = _db.PagamentosPix
            .Include(p => p.Presente)
            .FirstOrDefault(p => p.Id == id);

        if (pagamento == null)
            return NotFound();

        pagamento.Confirmado = true;
        pagamento.ConfirmadoEm = DateTime.Now;

        pagamento.Presente.Status = StatusPresente.Presenteado;

        _db.SaveChanges();
        return RedirectToPage();
    }

    public IActionResult OnPostRecusar(int id)
    {
        var pagamento = _db.PagamentosPix
            .FirstOrDefault(p => p.Id == id);

        if (pagamento == null)
            return NotFound();

        _db.PagamentosPix.Remove(pagamento);
        _db.SaveChanges();

        return RedirectToPage();
    }
}