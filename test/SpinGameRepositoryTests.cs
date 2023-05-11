using System;
using SpinGameApp.Model;
using SpinGameApp.Repositories;
using Xunit;

namespace SpinGameTest
{
    public class SpinGameRepositoryTests : IClassFixture<DbFixture>
    {
        private readonly DbFixture _fixture;

        public SpinGameRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_spin_game_should_have_an_id()
        {
            var repo = new SpinGameRepository(_fixture.DbSettings);
            var spinGame = new SpinGame
            {
                Name = "Spin1",
                Prizes = new List<Prize>
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
                }
            };
            var newSpinGameId = repo.Create(spinGame);
            Assert.NotEmpty(newSpinGameId);
        }

        [Fact]
        public void Get_spin_game_by_id_should_return_correct_result()
        {
            var repo = new SpinGameRepository(_fixture.DbSettings);
            var spinGame = repo.GetByName("Spin Game Test 1");
            Assert.NotNull(spinGame);

            var spinGame1 = repo.Get(spinGame.Id);

            Assert.NotNull(spinGame1);
            Assert.Equal("Spin Game Test 1", spinGame1.Name);
            Assert.Equal(2, spinGame1.Prizes.Count);
        }

        [Fact]
        public void Update_spin_game_should_success()
        {
            var repo = new SpinGameRepository(_fixture.DbSettings);
            var spinGame = repo.GetByName("Spin Game Test 1");
            Assert.NotNull(spinGame);

            spinGame.Name = "Spin Game 1";
            repo.Update(spinGame);
            var updatedSpinGame = repo.GetByName("Spin Game 1");

            Assert.NotNull(updatedSpinGame);
            Assert.Equal("Spin Game 1", updatedSpinGame.Name);
        }

        [Fact]
        public void Delete_spin_game_should_success()
        {
            var repo = new SpinGameRepository(_fixture.DbSettings);
            var spinGame = repo.GetByName("Spin Game Test 1");
            Assert.NotNull(spinGame);

            repo.Delete(spinGame.Id);
            var deletedSpinGame = repo.Get(spinGame.Id);

            Assert.Null(deletedSpinGame);
        }
    }
}