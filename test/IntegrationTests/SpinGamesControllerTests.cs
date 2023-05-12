using MongoDB.Bson;
using MongoDB.Driver;
using SpinGameApp.Model;
using SpinGameTest.Fixtures;
using Xunit;
using System.Net.Http.Json;

namespace SpinGameTest.IntegrationTests
{
    public class SpinGamesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>, IClassFixture<Mongo2GoFixture>
    {
        private readonly HttpClient _client;
        private readonly Mongo2GoFixture _mongoDb;

        public SpinGamesControllerTests(CustomWebApplicationFactory<Program> factory, Mongo2GoFixture mongoDb)
        {
            _mongoDb = mongoDb;
            _client = factory
                    .InjectMongoDbConfigurationSettings(mongoDb.ConnectionString, mongoDb.DatabaseName)
                    .CreateClient();

            _mongoDb.SeedData();
        }

        [Fact]
        public async Task Spin_should_return_a_spin_result_when_requirements_are_fulfilled()
        {
            var spinGame = await _mongoDb.SpinGameCollection
                    .Find(s => s.Name == "Spin Game Test 1")
                    .FirstOrDefaultAsync();
            Assert.NotNull(spinGame);

            var spinGameId = spinGame.Id;

            Assert.True(ObjectId.TryParse(spinGameId, out _));

            var response = await _client.PostAsync($"/api/spingames/spin?spinGameId={spinGameId}&playerId=player1", null);

            Assert.Equal(200, ((int)response.StatusCode));
            var spinResult = await response.Content.ReadFromJsonAsync<SpinResult>();

            Assert.NotNull(spinResult);
            Assert.Equal(spinGameId, spinResult.SpinGameId);
            Assert.Equal("player1", spinResult.PlayerId);
        }

    }
}