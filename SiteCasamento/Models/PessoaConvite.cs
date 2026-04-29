using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SiteCasamento.Models;

public class PessoaConvite
{
    public int Id { get; set; }
    public int ConviteId { get; set; }
    [ValidateNever]
    public Convite Convite { get; set; } = default!;
    public string Nome { get; set; } = string.Empty;
    public bool? Vai { get; set; }
    public DateTime? DataResposta { get; set; }
}