using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteCasamento.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Convites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeExibicao = table.Column<string>(type: "TEXT", nullable: false),
                    NomeNormalizado = table.Column<string>(type: "TEXT", nullable: false),
                    TelefoneUltimos4 = table.Column<string>(type: "TEXT", nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", nullable: true),
                    DataUltimaResposta = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    ImagemUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservadoPor = table.Column<string>(type: "TEXT", nullable: true),
                    DataReserva = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LinkCompra = table.Column<string>(type: "TEXT", nullable: true),
                    ObservacaoEntrega = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PessoasConvite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConviteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Vai = table.Column<bool>(type: "INTEGER", nullable: true),
                    DataResposta = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoasConvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoasConvite_Convites_ConviteId",
                        column: x => x.ConviteId,
                        principalTable: "Convites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PagamentosPix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PresenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Confirmado = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ConfirmadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NomeConvidado = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Loja = table.Column<string>(type: "TEXT", nullable: true),
                    NumeroPedido = table.Column<string>(type: "TEXT", nullable: true),
                    Mensagem = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentosPix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentosPix_Presentes_PresenteId",
                        column: x => x.PresenteId,
                        principalTable: "Presentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PagamentosPix_PresenteId",
                table: "PagamentosPix",
                column: "PresenteId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoasConvite_ConviteId",
                table: "PessoasConvite",
                column: "ConviteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PagamentosPix");

            migrationBuilder.DropTable(
                name: "PessoasConvite");

            migrationBuilder.DropTable(
                name: "Presentes");

            migrationBuilder.DropTable(
                name: "Convites");
        }
    }
}
