using SiteCasamento.Models;
using SiteCasamento.Services;

namespace SiteCasamento.Data;

public static class DatabaseSeeder
{
    public static void Seed(AppDbContext db)
    {
        /* =====================================================
           CONVITES + PESSOAS
        ===================================================== */
        if (!db.Convites.Any())
        {
            void AddConvite(string nome, string tel, params string[] pessoas)
            {
                var convite = new Convite
                {
                    NomeExibicao = nome,
                    NomeNormalizado = nome,
                    TelefoneUltimos4 = tel
                };

                db.Convites.Add(convite);
                db.SaveChanges();

                foreach (var p in pessoas)
                {
                    db.PessoasConvite.Add(new PessoaConvite
                    {
                        ConviteId = convite.Id,
                        Nome = p
                    });
                }

                db.SaveChanges();
            }

            AddConvite("Tia Mariana e Tio Gilberto","6611","Tia Mariana","Tio Gilberto");
            AddConvite("Wania e Gabriel","0974","Wania","Gabriel");
            AddConvite("Gilberto e Família","9384","Gilberto","Angelita","João Vitor","Miguel");
            AddConvite("Cássio e Família","3842","Cássio","Karina","Maria Júlia","Rafael");
            AddConvite("Tia Márcia e Família","1318","Tia Márcia","Tio Ricardo","Marcelo","Lorenzo");
            AddConvite("Mariane e Família","3939","Mariane","Luiz","Davi Luiz","Lucas Neto");
            AddConvite("Madrinha e Padrinho","4863","Madrinha (Aninha)","Padrinho (Tadeu)");
            AddConvite("Lucas e Família","1627","Lucas Silveira","Isadora","Joaquim","José","João Silveira");
            AddConvite("Michael e Família","8602","Michael","Mariana","Mariah","Maria Luiza");
            AddConvite("Tio Chico e Família","7456","Tio Chico","Geralda","Thiago","Thales","Matheus");
            AddConvite("Tio Fábio e Família","5808","Tio Fábio","Riccielle","Fabricio","Sara Silveira");
            AddConvite("Tio Zé","6174","Tio Zé","Tia Rejane","Fernanda","Totonho");
            AddConvite("Guilherme e Marcella","6178","Guilherme Silveira","Marcella");
            AddConvite("Tia Eudalia e Tio Lucimar","0339","Tia Eudalia","Tio Lucimar");
            AddConvite("Ana Paula e Família","0526","Ana Paula","Keky","Melyna","Lavinia");
            AddConvite("Bruno e Família","2850","Bruno","Lisa","Luiza","Gustavo");
            AddConvite("Guilherme Gomes e Família","5206","Guilherme Gomes","Tatiana","Julia","Arthur");
            AddConvite("Franciele e Família","6498","Franciele","Lucas Motta","Sara Gomes","Samuel","Beatriz");
            AddConvite("Raiane e Família","5437","Raiane","Felipe","Laura","Cecilia");
            AddConvite("Tia Nalva e Família","6451","Tia Nalva","Tio Daniel","Juninho");
            AddConvite("Adila e Família","7391","Adila","Vitor","Davi");
            AddConvite("Tuane e Família","1168","Tuane","Alan","Clara","Caio");
            AddConvite("Tia Edna e Tio Júlio","3910","Tia Edna","Tio Julio");
            AddConvite("Suellen e Família","7477","Suellen","Glauco","Isabelly","Isaac");
            AddConvite("Eduardo e Família","2732","Eduardo","Pamela","Loren","Levi");
            AddConvite("Tio Ernandes e Tia Eliana","0846","Tio Ernandes","Eliana");
            AddConvite("Tio Ernesto e Val","3061","Tio Ernesto","Val");
            AddConvite("Erica e Família","2064","Erica","Will","João Gomes","Helena");
            AddConvite("Clara e André","7068","Clara Elisi","André");
            AddConvite("Ana Rita","8828","Ana Rita");
            AddConvite("Débora","1797","Débora");
            AddConvite("Pedro","0510","Pedro");
            AddConvite("Talita","4782","Talita");
            AddConvite("Marília","2037","Marília");
            AddConvite("Tia Ana","6462","Tia Ana Alice");
            AddConvite("Tia Meire","1512","Tia Meire");
            AddConvite("Giuseppe e Luiza","9757","Giuseppe","Luiza Moreira");
            AddConvite("Giordano","0896","Giordano","Giordana");
            AddConvite("Lucas","0963","Lucas Soprani");
            AddConvite("Hugo e Nicolly","7702","Hugo","Nicolly");
            AddConvite("Amanda","1087","Amanda");
            AddConvite("Larissa","9839","Larissa");
            AddConvite("Rubio","7784","Rubio");
            AddConvite("Paulo","9809","Paulo");
            AddConvite("Mauricio","2209","Mauricio");
            AddConvite("Guilherme e Kaue","8122","Guilherme","Kauê");
        }

        /* =====================================================
           PRESENTES
        ===================================================== */
        if (!db.Presentes.Any())
        {
            db.Presentes.AddRange(
                new Presente { Nome="Chaleira Elétrica", Valor=243.80m, Status=StatusPresente.Disponivel, LinkCompra="https://www.amazon.com.br/Chaleira-El%C3%A9trica-Philco-Digital-PCH18B/dp/B0DMWPCJJX" },
                new Presente { Nome="Airfryer", Valor=749.00m, Status=StatusPresente.Disponivel, LinkCompra="https://www.amazon.com.br/Fritadeira-El%C3%A9trica-programada-girat%C3%B3rio-Electrolux/dp/B0BYK9KP3W" },
                new Presente { Nome="Panela Elétrica", Valor=180.00m, Status=StatusPresente.Disponivel, LinkCompra="https://www.mercadolivre.com.br/mondial-pe-43-pretoinox-127v-60-hz/p/MLB16030465" },
                new Presente { Nome="Multiprocessador", Valor=624.68m, Status=StatusPresente.Disponivel, LinkCompra="https://www.mercadolivre.com.br/p/MLB24039124" },
                new Presente { Nome="Mixer", Valor=189.90m, Status=StatusPresente.Disponivel, LinkCompra="https://www.amazon.com.br/Mixer-Philco-PMX2000-Inox-800W/dp/B0CFM6RMLM" },
                new Presente { Nome="Sanduicheira", Valor=0 },
                new Presente { Nome="Torradeira", Valor=0 },
                new Presente { Nome="Cafeteira", Valor=0 },
                new Presente { Nome="Lava Louças", Valor=1900 },
                new Presente { Nome="Filtro de Água", Valor=744.96m },
                new Presente { Nome="Robô Aspirador", Valor=0 },
                new Presente { Nome="Vaporizador de Roupas", Valor=149 },
                new Presente { Nome="Vaporizador Higienizador", Valor=189.90m },
                new Presente { Nome="Jogo de Panela Inox", Valor=0 },
                new Presente { Nome="Frigideira de Inox", Valor=211 },
                new Presente { Nome="Frigideira de Ferro", Valor=100.60m },
                new Presente { Nome="Jogo de Copos", Valor=0 },
                new Presente { Nome="Jogo de Taças", Valor=0 },
                new Presente { Nome="Jogo de Bowls", Valor=0 },
                new Presente { Nome="Aparelho de Jantar", Valor=0 },
                new Presente { Nome="Faqueiro", Valor=0 },
                new Presente { Nome="Jogo de Facas", Valor=113.05m },
                new Presente { Nome="Mandolim", Valor=0 },
                new Presente { Nome="Descascador", Valor=0 },
                new Presente { Nome="Assadeira", Valor=0 },
                new Presente { Nome="Tábua de Corte", Valor=0 },
                new Presente { Nome="Garrafa Térmica", Valor=67.72m },
                new Presente { Nome="Churrasqueira", Valor=0 },
                new Presente { Nome="Kit Churrasco", Valor=85.50m },
                new Presente { Nome="Moedor de Pimenta", Valor=60 },
                new Presente { Nome="Cesto de Roupa Suja", Valor=84.55m },
                new Presente { Nome="Tapete", Valor=0 },
                new Presente { Nome="Roupão", Valor=0 },
                new Presente { Nome="Mesa de Cabeceira", Valor=395 },
                new Presente { Nome="Cabeceira", Valor=0 },
                new Presente { Nome="Pillow Top", Valor=0 },
                new Presente { Nome="Abajur", Valor=0 },
                new Presente { Nome="Porta Retrato", Valor=0 },
                new Presente { Nome="Cadeira de Escritório", Valor=799 },
                new Presente { Nome="PIX", Valor=0 },
                new Presente { Nome="Pote de Vidro", Valor=0 }
            );

            db.SaveChanges();
        }
    }
}