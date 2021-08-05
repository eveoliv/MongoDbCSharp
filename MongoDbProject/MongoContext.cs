using MongoDB.Driver;

namespace MongoDbProject
{
    public class MongoContext
    {
        public const string CONNTECTION = "mongodb://localhost:27017";
        public const string DATABASE = "Biblioteca";
        public const string COLLECTION = "Livros";

        private static readonly IMongoClient client;
        private static readonly IMongoDatabase db;

        static MongoContext()
        {
            client = new MongoClient(CONNTECTION);
            db = client.GetDatabase(DATABASE);
        }

        public IMongoClient Cliente
        {
            get { return client; }
        }

        public IMongoCollection<Livro> Livros
        {
            get { return db.GetCollection<Livro>(COLLECTION); }
        }
    }
}
