using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;
using SiteCasamento.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Banco

//var dbPath = Path.Combine(builder.Environment.ContentRootPath, "casamento.db");

//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));


var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataDir);

var dbPath = Path.Combine(dataDir, "casamento.db");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));


builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<PixService>();

builder.Services.Configure<PixOptions>(builder.Configuration.GetSection("Pix"));

// Authentication (Cookie)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Para onde mandar se tentar acessar /Admin sem estar logado
        options.LoginPath = "/Admin/Login";

        // Boas práticas simples
        options.Cookie.HttpOnly = true;  // JS não acessa o cookie
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // só HTTPS
    });

// Authorization
builder.Services.AddAuthorization();

// Protege a pasta /Admin inteira, exceto a página de login
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin");
    options.Conventions.AllowAnonymousToPage("/Admin/Login");
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Ordem importa
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();

// Seed apenas em Development
//if (app.Environment.IsDevelopment())
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var db = scope.ServiceProvider.GetRequiredService<SiteCasamento.Data.AppDbContext>();

//        if (!db.Presentes.Any())
//        {
//            db.Presentes.AddRange(
//                new SiteCasamento.Models.Presente
//                {
//                    Nome = "Jogo de Panelas",
//                    Descricao = "Conjunto antiaderente para o dia a dia.",
//                    Valor = 450m,
//                    ImagemUrl = "/images/jogo-panelas.jpg"
//                },
//                new SiteCasamento.Models.Presente
//                {
//                    Nome = "Cafeteira",
//                    Descricao = "Cafeteira elétrica para cafés especiais.",
//                    Valor = 320m,
//                    ImagemUrl = "/images/jogo-panelas.jpg"
//                },
//                new SiteCasamento.Models.Presente
//                {
//                    Nome = "Air Fryer",
//                    Descricao = "Fritadeira sem óleo 4L.",
//                    Valor = 380m,
//                    ImagemUrl = "/images/jogo-panelas.jpg"
//                }
//            );

//            db.SaveChanges();
//        }
//        if (!db.Convites.Any())
//        {
//            var convite = new Convite
//            {
//                NomeExibicao = "Tio Fábio e Família",
//                NomeNormalizado = TextService.Normalizar("Tio Fábio e Família"),
//                TelefoneUltimos4 = "1234",
//                Pessoas = new()
//                {
//                    new PessoaConvite { Nome = "Fábio" },
//                    new PessoaConvite { Nome = "Ricciele" },
//                    new PessoaConvite { Nome = "Fabrício" },
//                    new PessoaConvite { Nome = "Laura" }
//                }
//            };

//            db.Convites.Add(convite);
//            db.SaveChanges();
//        }
//    }
//}