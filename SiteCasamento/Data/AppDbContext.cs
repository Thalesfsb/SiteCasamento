using Microsoft.EntityFrameworkCore;
using SiteCasamento.Models;

namespace SiteCasamento.Data;

public class AppDbContext : DbContext
{
    public DbSet<Presente> Presentes { get; set; }
    public DbSet<Convite> Convites { get; set; }
    public DbSet<PessoaConvite> PessoasConvite { get; set; }
    
    public DbSet<PagamentoPix> PagamentosPix { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Convite>()
                .HasMany(c => c.Pessoas)
                .WithOne(p => p.Convite)
                .HasForeignKey(p => p.ConviteId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}