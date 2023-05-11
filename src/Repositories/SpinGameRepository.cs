using MongoDB.Driver;
using SpinGameApp.Model;

namespace SpinGameApp.Repositories
{
    public class SpinGameRepository : ISpinGameRepository
    {
        private const string collectionName = "SpinGames";

        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<SpinGame> _spinGameCollection;

        public SpinGameRepository(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _db = client.GetDatabase(settings.DbName);
            _spinGameCollection = _db.GetCollection<SpinGame>(collectionName);
        }

        public string Create(SpinGame spinGame)
        {
            _spinGameCollection.InsertOne(spinGame);
            return spinGame.Id;
        }

        public void Delete(string id)
        {
            _spinGameCollection.DeleteOne(s => s.Id == id);
        }

        public IReadOnlyList<SpinGame> GetAll(int page, int pageSize)
        {
            return _spinGameCollection.Find<SpinGame>(_ => true)
                .Sort(Builders<SpinGame>.Sort.Ascending(x => x.Name))
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToList();
        }

        public SpinGame Get(string id)
        {
            return _spinGameCollection.Find<SpinGame>(s => s.Id == id).FirstOrDefault();
        }

        public SpinGame GetByName(string name)
        {
            return _spinGameCollection.Find<SpinGame>(s => s.Name == name).FirstOrDefault();
        }

        public void Update(SpinGame spinGame)
        {
            _spinGameCollection.ReplaceOne(s => s.Id == spinGame.Id, spinGame);
        }
    }
}