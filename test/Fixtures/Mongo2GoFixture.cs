using Mongo2Go;
using MongoDB.Driver;
using SpinGameApp.Model;

namespace SpinGameTest.Fixtures
{
    public class Mongo2GoFixture
    {
        public MongoClient Client { get; }
 
        public IMongoDatabase Database { get; }

        public string DatabaseName { get; private set; }
 
        public string ConnectionString { get; }
 
        private readonly MongoDbRunner _mongoRunner;
 
        private readonly string _databaseName = "spin-game-integration-test";
        private readonly string _collectionName = "SpinGames";
 
        public IMongoCollection<SpinGame> SpinGameCollection { get; }
 
        public Mongo2GoFixture()
        {
            // initializes the instance
            _mongoRunner = MongoDbRunner.Start();
 
            // store the connection string with the chosen port number
            ConnectionString = _mongoRunner.ConnectionString;
 
            // create a client and database for use outside the class
            Client = new MongoClient(ConnectionString);
 
            Database = Client.GetDatabase(_databaseName);

            DatabaseName = _databaseName;
 
            // initialize your collection
            SpinGameCollection = Database.GetCollection<SpinGame>(_collectionName);
        }
 
        public void SeedData()
        {
            var documentCount = SpinGameCollection.CountDocuments(Builders<SpinGame>.Filter.Empty);
            if (documentCount == 0)
            {
                var prizes = new List<Prize>
                    {
                        new Prize
                        {
                            Name = "Common",
                            Probability = 55
                        },
                        new Prize
                        {
                            Name = "Rare",
                            Probability = 75
                        },
                        new Prize
                        {
                            Name = "Epic",
                            Probability = 90
                        }
                    };
                    SpinGameCollection.InsertMany(
                        new List<SpinGame>{
                            new SpinGame{
                                Name = "Spin Game Test 1",
                                Prizes = prizes.Take(2).ToList()
                            },
                            new SpinGame{
                                Name = "Spin Game Test 2",
                                Prizes = prizes.TakeLast(2).ToList()
                            },
                            new SpinGame{
                                Name = "Spin Game Test 3",
                                Prizes = prizes
                            },
                            new SpinGame{
                                Name = "Spin Game Test 4",
                                Prizes = prizes
                            }
                        }
                    );
            }
        }

 
        public void Dispose()
        {
            _mongoRunner.Dispose();
        }
    }
}