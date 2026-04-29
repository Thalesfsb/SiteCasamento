using System.Globalization;
using System.Text;

namespace SiteCasamento.Services;

public static class TextService
{
    public static string Normalizar(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) 
            return string.Empty;

        texto = texto.Trim().ToLowerInvariant();

        // remove acentos
        var normalized = texto.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    public static string SomenteDigitos(string texto)
        => new string((texto ?? "").Where(char.IsDigit).ToArray());
}