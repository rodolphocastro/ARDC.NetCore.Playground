using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistence.Mock.Generators;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Persistence.Mock.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IModelGenerator<Game> _gameGenerator;

        public GameRepository(IModelGenerator<Game> gameGenerator)
        {
            _gameGenerator = gameGenerator ?? throw new ArgumentNullException(nameof(gameGenerator));
        }

        public Game Create(Game game)
        {
            if (string.IsNullOrWhiteSpace(game.Id))
                game.Id = Guid.NewGuid().ToString();

            return game;
        }

        public Task<Game> CreateAsync(Game game, CancellationToken ct) => Task.FromResult(Create(game));

        public void Delete(string id) { }

        public void Delete(Game game) { }

        public Task DeleteAsync(string id, CancellationToken ct) => Task.CompletedTask;

        public Task DeleteAsync(Game game, CancellationToken ct) => Task.CompletedTask;

        public Game Get(string id)
        {
            var game = _gameGenerator.Get();
            game.Id = id;
            return game;
        }

        public IList<Game> Get() => _gameGenerator.Get(2, 200);

        public Task<Game> GetAsync(string id, CancellationToken ct) => Task.FromResult(Get(id));

        public Task<IList<Game>> GetAsync(CancellationToken ct) => Task.FromResult(Get());

        public void Update(string id, Game game) { }

        public Task UpdateAsync(string id, Game game, CancellationToken ct) => Task.CompletedTask;
    }
}
