using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using SpinGameApp.Controllers;
using SpinGameApp.Model;
using SpinGameApp.Repositories;
using Xunit;

namespace SpinGameTest.UnitTests
{
    public class SpinGamesControllerTests
    {
        private readonly SpinGamesController _spinGamesController;
        private readonly Mock<ISpinGameRepository> _spinGameRepoMock = new Mock<ISpinGameRepository>();
        private readonly Mock<ISpinResultRepository> _spinResultRepoMock = new Mock<ISpinResultRepository>();

        public SpinGamesControllerTests()
        {
            _spinGamesController = new SpinGamesController(_spinGameRepoMock.Object, _spinResultRepoMock.Object);
        }

        [Fact]
        public void GetSpinGame_by_id_should_return_a_spin_game_when_exists()
        {
            var spinGameId = ObjectId.GenerateNewId().ToString();
            var spinGame = new SpinGame
            {
                Id = spinGameId,
                Name = "Spin Game Test",
                Prizes = new List<Prize> 
                {
                    new Prize 
                    {
                        Name = "Common",
                        Probability = 55,
                    }
                }
            };
            _spinGameRepoMock.Setup(x => x.Get(spinGameId)).Returns(spinGame);
            
            var spinGameResult = _spinGamesController.GetSpinGame(spinGameId);

            Assert.IsType<SpinGame>(spinGameResult.Value);
        }

        [Fact]
        public void GetSpinGame_by_id_should_return_not_found_when_not_exists()
        {
            var spinGameId = ObjectId.GenerateNewId().ToString();
            _spinGameRepoMock.Setup(x => x.Get(spinGameId)).Returns(() => null!);
            
            
            var spinGameResult = _spinGamesController.GetSpinGame(spinGameId);

            Assert.IsType<NotFoundResult>(spinGameResult.Result);
        }

        [Fact]
        public void Spin_should_return_a_spin_result_when_requirements_are_fulfilled()
        {
            var spinGameId = ObjectId.GenerateNewId().ToString();
            var spinResultId = ObjectId.GenerateNewId().ToString();
            var playerId = "player1";
            var spinGame = new SpinGame
            {
                Id = spinGameId,
                Name = "Spin Game Test",
                Prizes = new List<Prize> 
                {
                    new Prize 
                    {
                        Name = "Common",
                        Probability = 55,
                    }
                }
            };
            var spinResult = new SpinResult
            {
                PlayerId = playerId,
                SpinGameId = spinGameId,
                PrizeName = "Common",
                PrizeResult = 50,
                Timestamp = DateTime.UtcNow
            };
            _spinGameRepoMock.Setup(x => x.Get(spinGameId)).Returns(spinGame);
            _spinResultRepoMock.Setup(x => x.Create(It.IsAny<SpinResult>())).Returns(spinResultId);
            
            var spinActionResult = _spinGamesController.Spin(spinGameId, playerId);

            Assert.IsType<SpinResult>(spinActionResult.Value);
            Assert.NotNull(spinActionResult.Value);
            Assert.Contains(spinActionResult.Value.PrizeResult, new List<int>{30, 40, 50});
        }
    }
}