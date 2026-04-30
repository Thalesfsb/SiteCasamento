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

            AddConvite("Tia Mariana e Tio Gilberto", "6611", "Tia Mariana", "Tio Gilberto");
            AddConvite("Wania e Gabriel", "0974", "Wania", "Gabriel");
            AddConvite("Gilberto e Família", "9384", "Gilberto", "Angelita", "João Vitor", "Miguel");
            AddConvite("Cássio e Família", "3842", "Cássio", "Karina", "Maria Júlia", "Rafael");
            AddConvite("Tia Márcia e Família", "1318", "Tia Márcia", "Tio Ricardo", "Marcelo", "Lorenzo");
            AddConvite("Mariane e Família", "3939", "Mariane", "Luiz", "Davi Luiz", "Lucas Neto");
            AddConvite("Madrinha e Padrinho", "4863", "Madrinha (Aninha)", "Padrinho (Tadeu)");
            AddConvite("Lucas e Família", "1627", "Lucas Silveira", "Isadora", "Joaquim", "José", "João Silveira");
            AddConvite("Michael e Família", "8602", "Michael", "Mariana", "Mariah", "Maria Luiza");
            AddConvite("Tio Chico e Família", "7456", "Tio Chico", "Geralda", "Thiago", "Thales", "Matheus");
            AddConvite("Tio Fábio e Família", "5808", "Tio Fábio", "Riccielle", "Fabricio", "Sara Silveira");
            AddConvite("Tio Zé", "6174", "Tio Zé", "Tia Rejane", "Fernanda", "Totonho");
            AddConvite("Guilherme e Marcella", "6178", "Guilherme Silveira", "Marcella");
            AddConvite("Tia Eudalia e Tio Lucimar", "0339", "Tia Eudalia", "Tio Lucimar");
            AddConvite("Ana Paula e Família", "0526", "Ana Paula", "Keky", "Melyna", "Lavinia");
            AddConvite("Bruno e Família", "2850", "Bruno", "Lisa", "Luiza", "Gustavo");
            AddConvite("Guilherme Gomes e Família", "5206", "Guilherme Gomes", "Tatiana", "Julia", "Arthur");
            AddConvite("Franciele e Família", "6498", "Franciele", "Lucas Motta", "Sara Gomes", "Samuel", "Beatriz");
            AddConvite("Raiane e Família", "5437", "Raiane", "Felipe", "Laura", "Cecilia");
            AddConvite("Tia Nalva e Família", "6451", "Tia Nalva", "Tio Daniel", "Juninho");
            AddConvite("Adila e Família", "7391", "Adila", "Vitor", "Davi");
            AddConvite("Tuane e Família", "1168", "Tuane", "Alan", "Clara", "Caio");
            AddConvite("Tia Edna e Tio Júlio", "3910", "Tia Edna", "Tio Julio");
            AddConvite("Suellen e Família", "7477", "Suellen", "Glauco", "Isabelly", "Isaac");
            AddConvite("Eduardo e Família", "2732", "Eduardo", "Pamela", "Loren", "Levi");
            AddConvite("Tio Ernandes e Tia Eliana", "0846", "Tio Ernandes", "Eliana");
            AddConvite("Tio Ernesto e Val", "3061", "Tio Ernesto", "Val");
            AddConvite("Erica e Família", "2064", "Erica", "Will", "João Gomes", "Helena");
            AddConvite("Clara e André", "7068", "Clara Elisi", "André");
            AddConvite("Ana Rita", "8828", "Ana Rita");
            AddConvite("Débora", "1797", "Débora");
            AddConvite("Pedro", "0510", "Pedro");
            AddConvite("Talita", "4782", "Talita");
            AddConvite("Marília", "2037", "Marília");
            AddConvite("Tia Ana", "6462", "Tia Ana Alice");
            AddConvite("Tia Meire", "1512", "Tia Meire");
            AddConvite("Giuseppe e Luiza", "9757", "Giuseppe", "Luiza Moreira");
            AddConvite("Giordano", "0896", "Giordano", "Giordana");
            AddConvite("Lucas", "0963", "Lucas Soprani");
            AddConvite("Hugo e Nicolly", "7702", "Hugo", "Nicolly");
            AddConvite("Amanda", "1087", "Amanda");
            AddConvite("Larissa", "9839", "Larissa");
            AddConvite("Rubio", "7784", "Rubio");
            AddConvite("Paulo", "9809", "Paulo");
            AddConvite("Mauricio", "2209", "Mauricio");
            AddConvite("Guilherme e Kaue", "8122", "Guilherme", "Kauê");
        }

        /* =====================================================
           PRESENTES
        ===================================================== */
        if (!db.Presentes.Any())
        {
            db.Presentes.AddRange(

            new() { Nome = "Chaleira Elétrica", ImagemUrl = "/images/produtos/chaleiraeletrica.jpg", Valor = 243.80m, LinkCompra = "https://www.amazon.com.br/Chaleira-El%C3%A9trica-Philco-Digital-PCH18B/dp/B0DMWPCJJX" },
            new() { Nome = "Air Fryer", ImagemUrl = "/images/produtos/airfryer.jpg", Valor = 749.00m, LinkCompra = "https://www.amazon.com.br/Fritadeira-El%C3%A9trica-programada-girat%C3%B3rio-Electrolux/dp/B0BYK9KP3W" },
            new() { Nome = "Panela de Pressão Elétrica", ImagemUrl = "/images/produtos/paneladepressao.jpg", Valor = 506.24m, LinkCompra = "https://www.amazon.com.br/Electrolux-capacidade-silenciosa-seguran%C3%A7a-pr%C3%A9-programadas/dp/B076HYKFL7" },
            new() { Nome = "Panela Elétrica", ImagemUrl = "/images/produtos/panelaeletrica.jpg", Valor = 180.00m, LinkCompra = "https://www.mercadolivre.com.br/mondial-pe-43-pretoinox-127v-60-hz/p/MLB16030465" },
            new() { Nome = "Multiprocessador", ImagemUrl = "/images/produtos/multiprocessador.jpg", Valor = 624.68m, LinkCompra = "https://www.mercadolivre.com.br/p/MLB24039124" },
            new() { Nome = "Mixer", ImagemUrl = "/images/produtos/mixer.jpg", Valor = 189.90m, LinkCompra = "https://www.amazon.com.br/Mixer-Philco-PMX2000-Inox-800W/dp/B0CFM6RMLM" },
            new() { Nome = "Sanduicheira e Grill", ImagemUrl = "/images/produtos/sanduicheira.jpg", Valor = 306.48m, LinkCompra = "https://www.mercadolivre.com.br/grill-philco-pgr19pi-revestimento-redstone-7-temperaturas-preto/p/MLB23391635" },
            new() { Nome = "Torradeira", ImagemUrl = "/images/produtos/torradeira.jpg", Valor = 129.90m, LinkCompra = "https://www.amazon.com.br/Torradeira-French-Toast-Philco-56202010/dp/B076J17W32" },
            new() { Nome = "Cafeteira", ImagemUrl = "/images/produtos/cafeteira.jpg", Valor = 574.28m, LinkCompra = "https://www.mercadolivre.com.br/philco-pcf04a-eletrica-preto-127v/p/MLB57460090" },
            new() { Nome = "Purificador de Água", ImagemUrl = "/images/produtos/purificadordeagua.jpg", Valor = 744.96m, LinkCompra = "https://www.mercadolivre.com.br/p/MLB23745094" },

            new() { Nome = "Passadeira à Vapor", ImagemUrl = "/images/produtos/passadeira.jpg", Valor = 149.00m, LinkCompra = "https://www.amazon.com.br/Passadeira-Vapor-Port%C3%A1til-Mondial-Branco/dp/B0C59MB8V7" },
            new() { Nome = "Vaporizador Higienizador", ImagemUrl = "/images/produtos/vaporizador.jpg", Valor = 189.90m, LinkCompra = "https://www.mercadolivre.com.br/intech-machine-vapor-top-clean-amarelo-127v/p/MLB53926362" },

            new() { Nome = "Jogo de Panelas Inox", ImagemUrl = "/images/produtos/jogopanela.jpg", Valor = 679.90m, LinkCompra = "https://www.magazineluiza.com.br/jogo-de-panelas-tramontina-inox-fundo-triplo-5-pecas-solar/p/240138500/" },
            new() { Nome = "Frigideira de Inox", ImagemUrl = "/images/produtos/frigideirainox.jpg", Valor = 211.00m, LinkCompra = "https://a.co/d/0jflwSnA" },
            new() { Nome = "Frigideira de Ferro", ImagemUrl = "/images/produtos/frigideiraferro.jpg", Valor = 100.60m, LinkCompra = "https://a.co/d/08YZBFWB" },

            new() { Nome = "Jogo de Copos", ImagemUrl = "/images/produtos/jogodecopos.jpg", Valor = 37.15m, LinkCompra = "https://www.mercadolivre.com.br/p/MLB24705160" },
            new() { Nome = "Jogo de Taças", ImagemUrl = "/images/produtos/jogodetacas.jpg", Valor = 140.76m, LinkCompra = "https://www.mercadolivre.com.br/up/MLBU1719608501" },
            new() { Nome = "Jogo de Bowls", ImagemUrl = "/images/produtos/jogodebowls.jpg", Valor = 170.66m, LinkCompra = "https://www.amazon.com.br/Conjunto-Bowl-Org%C3%A2nico-Latte-558/dp/B0BV391Q2W" },
            new() { Nome = "Aparelho de Jantar", ImagemUrl = "/images/produtos/aparelhodejantar.jpg", Valor = 564.90m, LinkCompra = "https://www.amazon.com.br/dp/B0CV6J1K9R" },

            new() { Nome = "Faqueiro 30 Peças", ImagemUrl = "/images/produtos/faqueiro.jpg", Valor = 167.51m, LinkCompra = "https://www.amazon.com.br/Wolff-Faqueiro-Viena-Pe%C3%A7as-Prateado/dp/B0B1KVQ663" },
            new() { Nome = "Jogo de Facas", ImagemUrl = "/images/produtos/jogodefacas.jpg", Valor = 113.05m, LinkCompra = "https://www.amazon.com.br/Profissionais-Inoxid%C3%A1vel-Antiferrugem-Anticorros%C3%A3o-Inteiri%C3%A7as/dp/B0DZNR4Q2Q" },

            new() { Nome = "Mandolim", ImagemUrl = "/images/produtos/mandolim.jpg", Valor = 149.90m, LinkCompra = "https://www.mercadolivre.com.br/p/MLB47847511" },
            new() { Nome = "Kit Descascadores", ImagemUrl = "/images/produtos/kitdescascador.jpg", Valor = 36.90m, LinkCompra = "https://br.shp.ee/iSU6BC18" },
            new() { Nome = "Conjunto de Assadeiras", ImagemUrl = "/images/produtos/conjuntoassadeiras.jpg", Valor = 144.90m, LinkCompra = "https://shopee.com.br/Conjunto-Multi-Formas" },
            new() { Nome = "Tábua de Corte", ImagemUrl = "/images/produtos/tabuadecorte.jpg", Valor = 135.57m, LinkCompra = "https://www.mercadolivre.com.br/p/MLB50294615" },

            new() { Nome = "Garrafa Térmica", ImagemUrl = "/images/produtos/garrafatermica.jpg", Valor = 67.72m, LinkCompra = "https://www.mercadolivre.com.br/p/MLB26009042" },
            new() { Nome = "Kit Churrasco", ImagemUrl = "/images/produtos/kitchurrasco.jpg", Valor = 85.50m, LinkCompra = "https://a.co/d/08mnAirA" },
            new() { Nome = "Moedor de Pimenta", ImagemUrl = "/images/produtos/moedordepimenta.jpg", Valor = 60.00m, LinkCompra = "https://www.mercadolivre.com.br/up/MLBU3075033650" },

            new() { Nome = "Cesto de Roupa Suja", ImagemUrl = "/images/produtos/cestoderoupasuja.jpg", Valor = 84.55m, LinkCompra = "https://br.shp.ee/XYZ2HPPg" },
            new() { Nome = "Mesa de Cabeceira", ImagemUrl = "/images/produtos/mesacabeceira.jpg", Valor = 395.00m, LinkCompra = "https://www.mercadolivre.com.br/mesa-cabeceira-madeira-apoana" },
            new() { Nome = "Cadeira de Escritório", ImagemUrl = "/images/produtos/cadeiradeescritorio.jpg", Valor = 799.00m, LinkCompra = "https://www.flexform.com.br/cadeiras/cadeiras-de-escritorio/cadeira-dot-all-black" },

            // EXPERIÊNCIAS
            new() { Nome = "Massagem Relaxante", ImagemUrl = "/images/produtos/massagem.jpg", Valor = 300.00m },
            new() { Nome = "Passeio de Barco", ImagemUrl = "/images/produtos/passeiobarco.jpg", Valor = 300.00m },
            new() { Nome = "Transfer Aeroporto → Pousada", ImagemUrl = "/images/produtos/transfer.jpg", Valor = 400.00m },
            new() { Nome = "Aluguel de Bicicletas", ImagemUrl = "/images/produtos/bicicleta.jpg", Valor = 200.00m },
            new() { Nome = "Jantar Especial", ImagemUrl = "/images/produtos/jantar.jpg", Valor = 500.00m },

            // DECORAÇÃO / EXTRAS
            new() { Nome = "Kit Bandeja Decorativa", ImagemUrl = "/images/produtos/bandeja.jpg", Valor = 79.11m, LinkCompra = "https://br.shp.ee/VqmmULzf" },
            new() { Nome = "Bandeja com Pé", ImagemUrl = "/images/produtos/bandejacompe.jpg", Valor = 57.49m, LinkCompra = "https://www.amazon.com.br/Mesa-Servir-Bandeja-Manh%C3%A3-Bambu/dp/B0GGJ9DYHG" },
            new() { Nome = "Lava-Louças", ImagemUrl = "/images/produtos/lavaloucas.jpg", Valor = 1800.00m, LinkCompra = "https://www.amazon.com.br/Lava-Lou%C3%A7as-Servi%C3%A7os-Prata-Midea/dp/B0937GLHWN" },
            new() { Nome = "Alexa", ImagemUrl = "/images/produtos/alexa.jpg", Valor = 469.00m, LinkCompra = "https://www.amazon.com.br/echo-spot-despertador-inteligente-com-alexa-cor-preta/dp/B0C2RS4ZG6" },

            new() { Nome = "Luminária Decorativa", ImagemUrl = "/images/produtos/luminariadecorativa.jpg", Valor = 96.00m, LinkCompra = "https://br.shp.ee/x5sDVW5B" },
            new() { Nome = "Luminária de Mesa", ImagemUrl = "/images/produtos/luminariamesa.jpg", Valor = 129.90m, LinkCompra = "https://www.tokstok.com.br/luminaria-mesa-nozes-natural-tambor" },
            new() { Nome = "Kit Aromaterapia", ImagemUrl = "/images/produtos/aromaterapia.jpg", Valor = 72.42m, LinkCompra = "https://www.mercadolivre.com.br/kit-aromaterapia" },

            new() { Nome = "Pillow Top", ImagemUrl = "/images/produtos/pillowtop.jpg", Valor = 529.00m, LinkCompra = "https://www.zelo.com.br/pillow-top-casal-zelo-p1008658" },
            new() { Nome = "Travesseiro", ImagemUrl = "/images/produtos/travesseiro.jpg", Valor = 139.90m, LinkCompra = "https://www.zelo.com.br/travesseiro-altenburg-suporte-firme" },

            new() { Nome = "Suporte para Papel Toalha", ImagemUrl = "/images/produtos/papeltoalha.jpg", Valor = 64.59m, LinkCompra = "https://www.amazon.com.br/Mimo-Style-Inoxid%C3%A1vel-Qualidade-Organiza%C3%A7%C3%A3o/dp/B08HW6C9T8" },
            new() { Nome = "Organizador Porta Rolos", ImagemUrl = "/images/produtos/portarolo.jpg", Valor = 56.16m, LinkCompra = "https://www.mercadolivre.com.br/p/MLB67925664" },

            new() { Nome = "Açucareiro de Cerâmica", ImagemUrl = "/images/produtos/acucareiro.jpg", Valor = 66.41m, LinkCompra = "https://shopee.com.br/product/1064529684/23092369905" },
            new() { Nome = "Kit Coador + Manteigueira Francesa", ImagemUrl = "/images/produtos/coadormantegueira.jpg", Valor = 162.90m, LinkCompra = "https://shopee.com.br/Kit-Coador-de-Caf%C3%A9-e-Manteigueira" },
            new() { Nome = "Queijeira de Cerâmica", ImagemUrl = "/images/produtos/queijeira.jpg", Valor = 73.06m, LinkCompra = "https://shopee.com.br/Queijeira-em-Cer%C3%A2mica" },

            new() { Nome = "Travessa de Cerâmica", ImagemUrl = "/images/produtos/travessaceramica.jpg", Valor = 56.91m, LinkCompra = "https://shopee.com.br/Travessa-Toscana-Buffet-Cer%C3%A2mica" },
            new() { Nome = "Kit Travessas de Cerâmica", ImagemUrl = "/images/produtos/kittravessaceramica.jpg", Valor = 105.90m, LinkCompra = "https://shopee.com.br/Kit-Duplo-1-Travessa" },
            new() { Nome = "Molheira de Cerâmica", ImagemUrl = "/images/produtos/molheira.jpg", Valor = 30.95m, LinkCompra = "https://shopee.com.br/Molheira-Colonial-de-Cer%C3%A2mica" },
            new() { Nome = "Rocamboleira de Cerâmica", ImagemUrl = "/images/produtos/rocamboleira.jpg", Valor = 44.90m, LinkCompra = "https://shopee.com.br/Rocamboleira-Retangular-Grande-Cer%C3%A2mica" },

            // ITENS ADICIONADOS
            new() { Nome = "Kit Potes de Vidro Herméticos", ImagemUrl = "/images/produtos/kitpotes.jpg", Valor = 114.90m, LinkCompra = "https://shopee.com.br/Pote-De-Vidro-Premium-Herm%C3%A9tico" },
            new() { Nome = "Pote Hermético 2200ml", ImagemUrl = "/images/produtos/pote2200.jpg", Valor = 62.61m, LinkCompra = "https://shopee.com.br/POTE-DE-VIDRO-HERM%C3%89TICO" },
            new() { Nome = "Conjunto de Potes para Geladeira", ImagemUrl = "/images/produtos/potegeladeira.jpg", Valor = 227.90m, LinkCompra = "https://shopee.com.br/Kit-Conjunto" },
            new() { Nome = "Barra Magnética para Facas", ImagemUrl = "/images/produtos/barramagnetica.jpg", Valor = 89.90m, LinkCompra = "https://www.amazon.com.br/Barra-Magn%C3%A9tica-Facas" },
            new() { Nome = "Descascador de Alho", ImagemUrl = "/images/produtos/descascadoreletrico.jpg", Valor = 59.90m, LinkCompra = "https://br.shp.ee/fHfMB9xL" }

            );

            db.SaveChanges();
        }
    }
}