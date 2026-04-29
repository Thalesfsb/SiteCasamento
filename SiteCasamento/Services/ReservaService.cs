using Microsoft.EntityFrameworkCore;
using SiteCasamento.Data;
using SiteCasamento.Models;

namespace SiteCasamento.Services;

public class ReservaService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public ReservaService(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public void LiberarReservasExpiradas()
    {
        var ttlHoras = _config.GetValue<int>("Reserva:TtlHoras", 24);
        var limite = DateTime.Now.AddHours(-ttlHoras);

        // Libera apenas reservas SEM nenhum pagamento pendente (PIX ou Compra Externa)
        _db.Presentes
            .Where(p =>
                p.Status == StatusPresente.Reservado &&
                p.DataReserva < limite &&
                !_db.PagamentosPix.Any(pg =>
                    pg.PresenteId == p.Id &&
                    !pg.Confirmado)
            )
            .ExecuteUpdate(setters => setters
                .SetProperty(p => p.Status, StatusPresente.Disponivel)
                .SetProperty(p => p.ReservadoPor, (string?)null)
                .SetProperty(p => p.DataReserva, (DateTime?)null)
            );
    }
}