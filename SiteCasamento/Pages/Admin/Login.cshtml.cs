using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteCasamento.Pages.Admin;

public class LoginModel : PageModel
{
    private readonly IConfiguration _config;

    public LoginModel(IConfiguration config)
    {
        _config = config;
    }

    [BindProperty]
    public string Senha { get; set; } = "";

    public string? Erro { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        var senhaAdmin = _config["Admin:Senha"];

        if (string.IsNullOrEmpty(senhaAdmin) || Senha != senhaAdmin)
        {
            Erro = "Senha inválida.";
            return Page();
        }

        // Claims = “declarações” sobre o usuário
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "admin"),
            new Claim(ClaimTypes.Role, "Admin")
        };

        // Identity + Principal = usuário autenticado no ASP.NET
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        // SignIn = cria o cookie de autenticação
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

        return RedirectToPage("/Admin/Dashboard");
    }
}