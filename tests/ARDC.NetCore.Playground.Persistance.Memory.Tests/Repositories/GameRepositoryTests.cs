using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ARDC.NetCore.Playground.Persistance.Memory.Tests.Repositories
{
    public class GameRepositoryTests : IClassFixture<ServiceProviderFixture>
    {
        private readonly IModelGenerator<Game> _gameGenerator;
        private readonly IGameRepository _gameRepository;
        private readonly PlaygroundContext _context;

        public GameRepositoryTests(ServiceProviderFixture fixture)
        {
            _gameGenerator = fixture.Provider.GetService<IModelGenerator<Game>>();
            _gameRepository = fixture.Provider.GetService<IUnitOfWork>().GameRepository;
            _context = fixture.Provider.GetService<PlaygroundContext>();
        }

        /// <summary>
        /// It should be possible to create a new game.
        /// </summary>
        [Fact(DisplayName = "Create a New Game")]
        public void CreateGame()
        {
            var newGame = _gameGenerator.Get();
            int gameCount = _gameRepository.Get().Count;

            var createdGame = _gameRepository.Create(newGame);
            createdGame.Should()
                .NotBeNull("a game should be created").And
                .BeAssignableTo<Game>("it should inherit the Game class").And
                .BeEquivalentTo(newGame, "it should contain the same properties as the game we sent to the repository");

            _context.SaveChanges();

            _gameRepository.Get().Count.Should().BeGreaterThan(gameCount, "the transaction was commited");
        }

        /// <summary>
        /// It should be possible to create a new game (Async).
        /// </summary>
        [Fact(DisplayName = "Create a New Game (Async)")]
        public async Task CreateGameAsync()
        {
            var newGame = _gameGenerator.Get();
            int gameCount = _gameRepository.Get().Count;

            var createdGame = await _gameRepository.CreateAsync(newGame);
            createdGame.Should()
                .NotBeNull("a game should be created").And
                .BeAssignableTo<Game>("it should inherit the Game class").And
                .BeEquivalentTo(newGame, "it should contain the same properties as the game we sent to the repository");

            _context.SaveChanges();

            _gameRepository.Get().Count.Should().BeGreaterThan(gameCount, "the transaction wasn't commited");
        }

        /// <summary>
        /// It should be possible to delete a game by its id.
        /// </summary>
        [Fact(DisplayName = "Delete a Game by ID")]
        public void DeleteGameById()
        {
            var firstGame = _context.Games.FirstOrDefault();
            int gameCount = _context.Games.Count();

            Action act = new Action(() => _gameRepository.Delete(firstGame.Id));
            act.Should().NotThrow<Exception>("it should be possible to delete an existing game without issues");
            _context.SaveChanges();

            _context.Games.Count().Should().BeLessThan(gameCount, "a game should have been removed from the Context");
        }

        /// <summary>
        /// It should be possible to delete a game by its id.
        /// </summary>
        [Fact(DisplayName = "Delete a Game by ID (Async)")]
        public void DeleteGameByIdAsync()
        {
            var firstGame = _context.Games.FirstOrDefault();
            int gameCount = _context.Games.Count();

            Func<Task> act = new Func<Task>(() => _gameRepository.DeleteAsync(firstGame.Id));
            act.Should().NotThrow<Exception>("it should be possible to delete an existing game without issues");
            _context.SaveChanges();

            _context.Games.Count().Should().BeLessThan(gameCount, "a game should have been removed from the Context");
        }

        /// <summary>
        /// It should be possible to delete a game.
        /// </summary>
        [Fact(DisplayName = "Delete a Game")]
        public void DeleteGame()
        {
            var firstGame = _context.Games.FirstOrDefault();
            int gameCount = _context.Games.Count();

            Action act = new Action(() => _gameRepository.Delete(firstGame));
            act.Should().NotThrow<Exception>("it should be possible to delete an existing game without issues");
            _context.SaveChanges();

            _context.Games.Count().Should().BeLessThan(gameCount, "a game should have been removed from the Context");
        }

        /// <summary>
        /// It should be possible to delete a game by its id.
        /// </summary>
        [Fact(DisplayName = "Delete a Game (Async)")]
        public void DeleteGameAsync()
        {
            var firstGame = _context.Games.FirstOrDefault();
            int gameCount = _context.Games.Count();

            Func<Task> act = new Func<Task>(() => _gameRepository.DeleteAsync(firstGame));
            act.Should().NotThrow<Exception>("it should be possible to delete an existing game without issues");
            _context.SaveChanges();

            _context.Games.Count().Should().BeLessThan(gameCount, "a game should have been removed from the Context");
        }

        /// <summary>
        /// It should be possible to get all games.
        /// </summary>
        [Fact(DisplayName = "Get all Games")]
        public void GetGames()
        {            
            int gameCount = _context.Games.Count();

            var games = _gameRepository.Get();
            games.Should()
                .NotBeNull("an object is always expected").And
                .BeAssignableTo<IList<Game>>("it should implement IList").And
                .BeOfType<List<Game>>("it should be a List of Games").And
                .NotBeEmpty("it should have some records because the Context was Seeded").And
                .HaveCount(gameCount, "it should have the same amount of games as the context");
        }

        /// <summary>
        /// It should be possible to get all games.
        /// </summary>
        [Fact(DisplayName = "Get all Games (Async)")]
        public async Task GetGamesAsync()
        {
            int gameCount = _context.Games.Count();

            var games = await _gameRepository.GetAsync();
            games.Should()
                .NotBeNull("an object is always expected").And
                .BeAssignableTo<IList<Game>>("it should implement IList").And
                .BeOfType<List<Game>>("it should be a List of Games").And
                .NotBeEmpty("it should have some records because the Context was Seeded").And
                .HaveCount(gameCount, "it should have the same amount of games as the context");
        }

        /// <summary>
        /// It should be possible to get a single game.
        /// </summary>
        [Fact(DisplayName = "Get a Game")]
        public void GetGame()
        {
            var firstGame = _context.Games.FirstOrDefault();

            var foundGame = _gameRepository.Get(firstGame.Id);
            foundGame.Should()
                .NotBeNull("an object is expected").And
                .BeAssignableTo<Game>("it should inherit Game").And
                .BeEquivalentTo(firstGame, "it should be the same game we got from the context");
        }

        /// <summary>
        /// It should be possible to get a single game.
        /// </summary>
        [Fact(DisplayName = "Get a Game (Async)")]
        public async Task GetGameAsync()
        {
            var firstGame = _context.Games.FirstOrDefault();

            var foundGame = await _gameRepository.GetAsync(firstGame.Id);
            foundGame.Should()
                .NotBeNull("an object is expected").And
                .BeAssignableTo<Game>("it should inherit Game").And
                .BeEquivalentTo(firstGame, "it should be the same game we got from the context");
        }

        /// <summary>
        /// It should be possible to update a game.
        /// </summary>
        [Fact(DisplayName = "Update a Game")]
        public void UpdateGame()
        {
            var firstGame = _context.Games.FirstOrDefault();
            string originalName = firstGame.Name;
            firstGame.Name = "a new name";

            Action act = new Action(() => _gameRepository.Update(firstGame.Id, firstGame));
            act.Should().NotThrow<Exception>("it should update an entity without issues");
            _context.SaveChanges();

            var foundGame = _context.Games.Where(g => g.Id == firstGame.Id).SingleOrDefault();
            foundGame.Name.Should().NotBe(originalName, "its name should have been changed in the context");
        }

        /// <summary>
        /// It should be possible to update a game.
        /// </summary>
        [Fact(DisplayName = "Update a Game (Async)")]
        public void UpdateGameAsync()
        {
            var firstGame = _context.Games.FirstOrDefault();
            string originalName = firstGame.Name;
            firstGame.Name = "a new name";

            Func<Task> act = new Func<Task>(() => _gameRepository.UpdateAsync(firstGame.Id, firstGame));
            act.Should().NotThrow<Exception>("it should update an entity without issues");
            _context.SaveChanges();

            var foundGame = _context.Games.Where(g => g.Id == firstGame.Id).SingleOrDefault();
            foundGame.Name.Should().NotBe(originalName, "its name should have been changed in the context");
        }
    }
}
