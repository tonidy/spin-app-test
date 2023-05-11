using MongoDB.Driver;
using SpinGameApp.Model;

namespace SpinGameApp.Repositories
{
    public class SpinResultRepository : ISpinResultRepository
    {
        private const string collectionName = "SpinResults";
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<SpinResult> _spinResultCollection;

        public SpinResultRepository(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _db = client.GetDatabase(settings.DbName);
            _spinResultCollection = _db.GetCollection<SpinResult>(collectionName);
        }

        public string Create(SpinResult spinResult)
        {
             _spinResultCollection.InsertOne(spinResult);
            return spinResult.Id;
        }

        public IReadOnlyList<SpinResult> GetByPlayerId(string playerId)
        {
            return _spinResultCollection.Find(s => s.PlayerId == playerId).ToList();
        }

        public IReadOnlyList<SpinResult> GetBySpinGameId(string spinGameId)
        {
            return _spinResultCollection.Find(s => s.SpinGameId == spinGameId).ToList();
        }
    }
}