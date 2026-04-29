    namespace SiteCasamento.Models;

public enum StatusPresente
{
    Disponivel = 0,
    Reservado = 1,
    Presenteado = 2
}

public class Presente
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    // Ex: "/images/panelas.jpg"
    public string ImagemUrl { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public StatusPresente Status { get; set; } = StatusPresente.Disponivel;

    // Para controle simples (sem tabela extra por enquanto)
    public string? ReservadoPor { get; set; }
    public DateTime? DataReserva { get; set; }

    public string? LinkCompra { get; set; } // link Mercado Livre/Amazon
    public string? ObservacaoEntrega { get; set; } // ex: "Cor preta", "Tamanho G" etc (opcional)

}
