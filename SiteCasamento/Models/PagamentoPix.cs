using System;

namespace SiteCasamento.Models
{
    public class PagamentoPix
    {
        public int Id { get; set; }

        public int PresenteId { get; set; }
        public Presente Presente { get; set; } = default!;

        // Controle manual
        public bool Confirmado { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? ConfirmadoEm { get; set; }

        // Quem informou/pagou
        public string NomeConvidado { get; set; } = "";

        // Tipo do pagamento pendente
        public TipoPagamento Tipo { get; set; }

        // Extras (apenas quando Tipo = CompraExterna)
        public string? Loja { get; set; }
        public string? NumeroPedido { get; set; }
        public string? Mensagem { get; set; }
    }

    public enum TipoPagamento
    {
        Pix = 1,
        CompraExterna = 2
    }
}