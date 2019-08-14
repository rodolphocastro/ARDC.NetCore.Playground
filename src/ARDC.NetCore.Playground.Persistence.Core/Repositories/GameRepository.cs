using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Persistence.Core.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly PlaygroundContext _context;

        public GameRepository(PlaygroundContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // TODO: Adicionar validações aos métodos
        public Game Create(Game game)
        {
            var newGame = _context.Games.Add(game);
            return newGame.Entity;
        }

        public Task<Game> CreateAsync(Game game, CancellationToken ct) => Task.FromResult(Create(game));        

        public void Delete(string id)
        {
            var game = _context.Games.Where(g => g.Id == id).SingleOrDefault();
            Delete(game);
        }

        public void Delete(Game game) => _context.Games.Remove(game);

        public async Task DeleteAsync(string id, CancellationToken ct)
        {
            var game = await GetAsync(id, ct);
            await DeleteAsync(game, ct);
        }

        public Task DeleteAsync(Game game, CancellationToken ct) => Task.FromResult(_context.Games.Remove(game));

        public Game Get(string id)
        {
            var game = _context.Games.Where(g => g.Id == id).SingleOrDefault();
            return game;
        }

        public IList<Game> Get() => _context.Games.ToList();

        public async Task<Game> GetAsync(string id, CancellationToken ct)
        {
            var game = await _context.Games.Where(g => g.Id == id).SingleOrDefaultAsync();
            return game;
        }

        public async Task<IList<Game>> GetAsync(CancellationToken ct)
        {
            var games = await _context.Games.ToListAsync(ct);
            return games;
        }

        public void Update(string id, Game game) => _context.Entry(game).State = EntityState.Modified;

        public Task UpdateAsync(string id, Game game, CancellationToken ct)
        {
            Update(id, game);
            return Task.CompletedTask;
        }
    }
}
