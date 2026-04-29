using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using SiteCasamento.Data;
using SiteCasamento.Models;
using SiteCasamento.Services;

public class PixModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly PixService _pix;

    public PixModel(AppDbContext db, PixService pix)
    {
        _db = db;
        _pix = pix;
    }

    public Presente Presente { get; private set; } = default!;
    public string Payload { get; private set; } = string.Empty;
    public string QrCodeBase64 { get; private set; } = string.Empty;

    public IActionResult OnGet(int id)
    {
        // Carrega o presente
        Presente = _db.Presentes.FirstOrDefault(p => p.Id == id);
        if (Presente == null)
            return NotFound();

        // Se já foi presenteado, não permite novo PIX
        if (Presente.Status == StatusPresente.Presenteado)
            return RedirectToPage("/Presentes");

        // Obtém payload PIX (fixo / configurado)
        Payload = _pix.ObterPayload();

        // Verifica se já existe PIX pendente para este presente
        var pagamentoPix = _db.PagamentosPix
            .FirstOrDefault(p =>
                p.PresenteId == id &&
                !p.Confirmado &&
                p.Tipo == TipoPagamento.Pix);

        // Cria pagamento PIX apenas se não existir
        if (pagamentoPix == null)
        {
            pagamentoPix = new PagamentoPix
            {
                PresenteId = id,
                Tipo = TipoPagamento.Pix,
                CriadoEm = DateTime.Now,
                Confirmado = false,
                NomeConvidado = Presente.ReservadoPor ?? string.Empty
            };

            _db.PagamentosPix.Add(pagamentoPix);
            _db.SaveChanges();
        }

        // Gera QR Code
        using var generator = new QRCodeGenerator();
        using var data = generator.CreateQrCode(
            Payload,
            QRCodeGenerator.ECCLevel.Q);

        var qr = new PngByteQRCode(data);
        QrCodeBase64 = Convert.ToBase64String(qr.GetGraphic(20));

        return Page();
    }
}