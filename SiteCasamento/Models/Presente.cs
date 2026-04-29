using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "O nome do presente é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? ImagemUrl { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser maior ou igual a zero.")]
    public decimal Valor { get; set; }
    public StatusPresente Status { get; set; } = StatusPresente.Disponivel;
    // Controle simples
    public string? ReservadoPor { get; set; }
    public DateTime? DataReserva { get; set; }
    public string? LinkCompra { get; set; }
    public string? ObservacaoEntrega { get; set; }
}