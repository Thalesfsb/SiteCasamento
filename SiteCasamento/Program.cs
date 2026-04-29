using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;
using SiteCasamento.Services;

var builder = WebApplication.CreateBuilder(args);

/* =====================================================
   CONFIGURAÇÃO (LOCAL + AZURE)
===================================================== */
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables(); // Admin__Senha no Azure

/* =====================================================
   RAZOR PAGES + AUTHORIZATION
===================================================== */
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin");
    options.Conventions.AllowAnonymousToPage("/Admin/Login");
});

/* =====================================================
   SESSION (PODE SER USADA EM OUTROS FLUXOS)
===================================================== */
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

/* =====================================================
   BANCO DE DADOS (SQLITE EM App_Data)
===================================================== */
var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
Directory.CreateDirectory(dataDir);

var dbPath = Path.Combine(dataDir, "casamento.db");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);

/* =====================================================
   SERVICES
===================================================== */
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<PixService>();

builder.Services.Configure<PixOptions>(
    builder.Configuration.GetSection("Pix")
);

/* =====================================================
   AUTHENTICATION (COOKIE)
===================================================== */
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization();

/* =====================================================
   BUILD
===================================================== */
var app = builder.Build();

/* =====================================================
   CRIA O BANCO E AS TABELAS NO STARTUP (AZURE)
===================================================== */
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    DatabaseSeeder.Seed(db);
}

/* =====================================================
   PIPELINE
===================================================== */
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ORDEM CORRETA
app.UseSession();
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