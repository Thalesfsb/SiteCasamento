using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SiteCasamento.Pages.Admin;

public class LoginModel : PageModel
{
    private readonly IConfiguration _configuration;

    public LoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public string Senha { get; set; } = string.Empty;

    public string? Erro { get; set; }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        var senhaAdmin = _configuration["Admin:Senha"];

        if (string.IsNullOrWhiteSpace(senhaAdmin))
        {
            Erro = "Senha do administrador não configurada.";
            return Page();
        }

        if (Senha != senhaAdmin)
        {
            Erro = "Senha inválida.";
            return Page();
        }

        // CRIA IDENTIDADE
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "Admin")
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        // CRIA COOKIE DE AUTENTICAÇÃO
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        // REDIRECIONA CORRETAMENTE
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToPage("/Admin/Dashboard");
    }

    public async Task<IActionResult> OnGetLogout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToPage("/Admin/Login");
    }
}