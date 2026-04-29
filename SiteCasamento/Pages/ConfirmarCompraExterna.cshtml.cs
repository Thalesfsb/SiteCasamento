using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteCasamento.Data;
using SiteCasamento.Models;

public class ConfirmarCompraExternaModel : PageModel
{
    private readonly AppDbContext _db;

    public ConfirmarCompraExternaModel(AppDbContext db)
    {
        _db = db;
    }

    public Presente? Presente { get; private set; }

    [BindProperty]
    public string Nome { get; set; } = string.Empty;

    [BindProperty]
    public string? Loja { get; set; }

    [BindProperty]
    public string? NumeroPedido { get; set; }

    [BindProperty]
    public string? Mensagem { get; set; }

    public bool Enviado { get; private set; }
    public string? Erro { get; private set; }

    public IActionResult OnGet(int id)
    {
        Presente = _db.Presentes.FirstOrDefault(p => p.Id == id);
        if (Presente == null)
            return NotFound();

        // Se já foi presenteado, não permite nova confirmação
        if (Presente.Status == StatusPresente.Presenteado)
            return RedirectToPage("/Presentes");

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        Presente = _db.Presentes.FirstOrDefault(p => p.Id == id);
        if (Presente == null)
            return NotFound();

        if (Presente.Status == StatusPresente.Presenteado)
            return RedirectToPage("/Presentes");

        if (string.IsNullOrWhiteSpace(Nome))
        {
            Erro = "Informe seu nome.";
            return Page();
        }

        var nome = Nome.Trim();

        // Verifica se já existe compra externa pendente para este presente
        var jaExiste = _db.PagamentosPix.Any(p =>
            p.PresenteId == id &&
            !p.Confirmado &&
            p.Tipo == TipoPagamento.CompraExterna);

        if (!jaExiste)
        {
            var pagamento = new PagamentoPix
            {
                PresenteId = id,
                Tipo = TipoPagamento.CompraExterna,
                CriadoEm = DateTime.Now,
                Confirmado = false,

                NomeConvidado = nome,
                Loja = Loja?.Trim(),
                NumeroPedido = NumeroPedido?.Trim(),
                Mensagem = Mensagem?.Trim()
            };

            _db.PagamentosPix.Add(pagamento);
        }

        // ? Mantém/renova a reserva do presente
        if (Presente.Status == StatusPresente.Disponivel)
            Presente.Status = StatusPresente.Reservado;

        Presente.ReservadoPor = nome;
        Presente.DataReserva = DateTime.Now;

        _db.SaveChanges();

        Enviado = true;
        return Page();
    }
}