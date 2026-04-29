namespace SiteCasamento.Models;

public class Convite
{
    public int Id { get; set; }

    // Ex: "Tio Fábio e Família"
    public string NomeExibicao { get; set; } = string.Empty;

    // Ex: "tio fabio e familia" (sem acento) -> facilita busca
    public string NomeNormalizado { get; set; } = string.Empty;

    // Guardar apenas os 4 últimos (privacidade + necessidade do seu caso)
    public string TelefoneUltimos4 { get; set; } = string.Empty;

    public string? Mensagem { get; set; }
    public DateTime? DataUltimaResposta { get; set; }

    public List<PessoaConvite> Pessoas { get; set; } = new();
}