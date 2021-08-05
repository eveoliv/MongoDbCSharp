using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MongoDbProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("");          

        }

        private static async Task DeletandoUmDocumento()
        {
            var context = new MongoContext();
            var construtor = Builders<Livro>.Filter;

            var condicao = construtor.Eq(x => x.Titulo, "Guerra dos Tronos");

            var listalivros = await context.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());
            }

            await context.Livros.DeleteManyAsync(condicao);
        }

        private static async Task AlterandoUmDocumentoComUpDate()
        {
            var context = new MongoContext();
            var construtor = Builders<Livro>.Filter;

            var condicao = construtor.Eq(x => x.Titulo, "Guerra dos Tronos");

            var listalivros = await context.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());               
            }

            var update = Builders<Livro>.Update;
            var updateCondicao = update.Set(x => x.Ano, 2001);

            //altera o documento conforme condicao
            await context.Livros.UpdateOneAsync(condicao, updateCondicao);
            //altera todos os documentos conforme condicao
            //await context.Livros.UpdateManyAsync(condicao, updateCondicao);

        }

        private static async Task AlterandoUmDocumento()
        {
            var context = new MongoContext();
            var construtor = Builders<Livro>.Filter;

            var condicao = construtor.Eq(x => x.Titulo, "Guerra dos Tronos");

            var listalivros = await context.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());
                doc.Ano = 2000;
                doc.Paginas = 900;

                await context.Livros.ReplaceOneAsync(condicao, doc);
            }

            Console.WriteLine("Fim da Lista");
        }

        private static async Task BuscaUtilizandoSortBy()
        {
            var context = new MongoContext();
            var construtor = Builders<Livro>.Filter;

            var condicao = construtor.AnyEq(x => x.Assunto, "Ação");

            var listalivros = await context.Livros.Find(condicao).SortBy(x => x.Titulo).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());
            }

            Console.WriteLine("Fim da Lista");
        }

        private static async Task BuscaDentroDeUmArrayDeColecao()
        {
            var context = new MongoContext();
            var construtor = Builders<Livro>.Filter;

            var condicao = construtor.AnyEq(x => x.Assunto, "Ação");

            var listalivros = await context.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());
            }

            Console.WriteLine("Fim da Lista");
        }

        private static async Task BuscaComDuasCondicoes()
        {
            var context = new MongoContext();
            var construtor = Builders<Livro>.Filter;

            var condicao = construtor.Gte(x => x.Ano, 1999) & construtor.Gte(y => y.Paginas, 300);

            var listalivros = await context.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());
            }

            Console.WriteLine("Fim da Lista");
        }

        private static async Task BuscaUtilizandoGraterThan()
        {
            var context = new MongoContext();
            var construtor = Builders<Livro>.Filter;

            var condicao = construtor.Gte(x => x.Ano, 1999);

            var listalivros = await context.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());
            }

            Console.WriteLine("Fim da Lista");
        }

        private static async Task BuscaUtilizandoBuilders()
        {
            var context = new MongoContext();
            var construtor = Builders<Livro>.Filter;

            var condicao = construtor.Eq(x => x.Autor, "Machado de Assis");

            var listalivros = await context.Livros.Find(condicao).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());
            }

            Console.WriteLine("Fim da Lista");
        }

        private static async Task BuscaUtilizandoFiltro()
        {
            var context = new MongoContext();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Listando Documentos Autor = Machado de Assis");

            var Filtro = new BsonDocument
            {
                {"Autor", "Machado de Assis"}
            };

            var listalivros = await context.Livros.Find(Filtro).ToListAsync();

            foreach (var doc in listalivros)
            {
                Console.WriteLine(doc.ToJson());
            }

            Console.WriteLine("Fim da Lista");
        }

        private static async Task ObtendoAListaDeDocumentos()
        {
            var context = new MongoContext();

            Console.WriteLine("Listando Documentos");

            var lista = await context.Livros.Find(new BsonDocument()).ToListAsync();

            foreach (var item in lista)
            {
                Console.WriteLine(item.ToJson<Livro>());
            }
        }

        private static async Task InclusaoLivrosEmLote()
        {
            var context = new MongoContext();

            List<Livro> Livros = new List<Livro>
            {
                ValoresLivro.IncluiValoresLivro("A Dança com os Dragões", "George R R Martin", 2011, 934, "Fantasia, Ação"),
                ValoresLivro.IncluiValoresLivro("A Tormenta das Espadas", "George R R Martin", 2006, 1276, "Fantasia, Ação"),
                ValoresLivro.IncluiValoresLivro("Memórias Póstumas de Brás Cubas", "Machado de Assis", 1915, 267, "Literatura Brasileira"),
                ValoresLivro.IncluiValoresLivro("Star Trek Portal do Tempo", "Crispin A C", 2002, 321, "Fantasia, Ação"),
                ValoresLivro.IncluiValoresLivro("Star Trek Enigmas", "Dedopolus Tim", 2006, 195, "Ficção Científica, Ação"),
                ValoresLivro.IncluiValoresLivro("Emília no Pais da Gramática", "Monteiro Lobato", 1936, 230, "Infantil, Literatura Brasileira, Didático"),
                ValoresLivro.IncluiValoresLivro("Chapelzinho Amarelo", "Chico Buarque", 2008, 123, "Infantil, Literatura Brasileira"),
                ValoresLivro.IncluiValoresLivro("20000 Léguas Submarinas", "Julio Verne", 1894, 256, "Ficção Científica, Ação"),
                ValoresLivro.IncluiValoresLivro("Primeiros Passos na Matemática", "Mantin Ibanez", 2014, 190, "Didático, Infantil"),
                ValoresLivro.IncluiValoresLivro("Saúde e Sabor", "Yeomans Matthew", 2012, 245, "Culinária, Didático"),
                ValoresLivro.IncluiValoresLivro("Goldfinger", "Iam Fleming", 1956, 267, "Espionagem, Ação"),
                ValoresLivro.IncluiValoresLivro("Da Rússia com Amor", "Iam Fleming", 1966, 245, "Espionagem, Ação"),
                ValoresLivro.IncluiValoresLivro("O Senhor dos Aneis", "J R R Token", 1948, 1956, "Fantasia, Ação")
            };

            await context.Livros.InsertManyAsync(Livros);
        }

        private static async Task IncluindoMaisDeHumLivro()
        {
            var context = new MongoContext();

            var Livro = new Livro();
            Livro = ValoresLivro.IncluiValoresLivro("Dom Casmurro", "Machado de Assis", 1923, 188, "Romance, Literatura  Brasileira");

            var Livro2 = new Livro();
            Livro2 = ValoresLivro.IncluiValoresLivro("A Arte da Ficção", "David Lodge", 2002, 230, "Didático, Auto Ajuda");

            await context.Livros.InsertOneAsync(Livro);
            await context.Livros.InsertOneAsync(Livro2);
        }

        private static async Task CriandoLivroComContext()
        {
            var Livro = new Livro
            {
                Titulo = "Star Wars Legends",
                Autor = "Timothy Zahn",
                Ano = 2010,
                Paginas = 245,
                Assunto = new List<string>
                {
                    "Ficção",
                    "Ação"
                }
            };

            var context = new MongoContext();

            await context.Livros.InsertOneAsync(Livro);
        }

        private static async Task AssessarMongoECriarColecao()
        {

            var doc = new BsonDocument
            {
                {"Titulo", "Guerra dos Tronos" }
            };

            doc.Add("Autor", "George R R");
            doc.Add("Ano", 1999);
            doc.Add("Paginas", 856);

            var assunto = new BsonArray();
            assunto.Add("Fantasia");
            assunto.Add("Ação");

            doc.Add("Assunto", assunto);

            //acesso ao mongodb
            var connection = "mongodb://localhost:27017";
            var client = new MongoClient(connection);

            //acesso ao banco 
            var db = client.GetDatabase("Biblioteca");

            //acesso a colecao
            var colecao = db.GetCollection<BsonDocument>("Livros");

            //incluir documento
            await colecao.InsertOneAsync(doc);
        }
    }
}
