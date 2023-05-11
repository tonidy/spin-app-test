using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SpinGameApp;
using SpinGameApp.Model;

namespace SpinGameTest
{
    public class DbFixture : IDisposable
    {
        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public DbFixture()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true)
               .AddJsonFile("appsettings.Development.json", optional: true)
               .Build();

            var connString = config.GetConnectionString("Mongodb");
            if (string.IsNullOrEmpty(connString))
                throw new ArgumentNullException(nameof(connString));

            var dbName = $"spin_game_test_{Guid.NewGuid()}";

            this.DbSettings = new MongoDbSettings(connString, dbName);

            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    var client = new MongoClient(DbSettings.ConnectionString);
                    var db = client.GetDatabase(DbSettings.DbName);
                    var spinGameCollection = db.GetCollection<SpinGame>("SpinGames");
                    var prizes = new List<Prize>
                    {
                        new Prize
                        {
                            Name = "Common",
                            Probability = 50
                        },
                        new Prize
                        {
                            Name = "Rare",
                            Probability = 10
                        },
                        new Prize
                        {
                            Name = "Epic",
                            Probability = 30
                        }
                    };
                    spinGameCollection.InsertMany(
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
                            }
                        }
                    );

                    _databaseInitialized = true;
                }
            }
        }

        public MongoDbSettings DbSettings { get; }

        public void Dispose()
        {
            var client = new MongoClient(this.DbSettings.ConnectionString);
            client.DropDatabase(this.DbSettings.DbName);
        }
    }
}