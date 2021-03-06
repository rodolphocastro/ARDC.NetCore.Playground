﻿using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistance.Mock.Generators;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ARDC.NetCore.Playground.Persistance.Mock.Tests.Repositories
{
    public class GameRepositoryTest : IClassFixture<ServiceProviderFixture>
    {
        private readonly IModelGenerator<Game> _gameGenerator;
        private readonly IGameRepository _gameRepo;

        public GameRepositoryTest(ServiceProviderFixture fixture)
        {
            _gameGenerator = fixture.Provider.GetService<IModelGenerator<Game>>();
            _gameRepo = fixture.Provider.GetService<IUnitOfWork>().GameRepository;
        }

        /// <summary>
        /// It should be possible to create a new game.
        /// </summary>
        [Fact(DisplayName = "Create a Game")]
        public void CreateGame()
        {
            var newGame = _gameGenerator.Get();
            newGame.Id = string.Empty;  // Set the ID as Empty so we can assert the Repository actually did something

            newGame = _gameRepo.Create(newGame);

            newGame.Id.Should().NotBeNullOrWhiteSpace("should be created by the Repository");
        }

        /// <summary>
        /// It should be possible to create a new game.
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Create a Game Async")]
        public async Task CreateGameAsync()
        {
            var newGame = _gameGenerator.Get();
            newGame.Id = string.Empty;  // Set the ID as Empty so we can assert the Repository actually did something

            newGame = await _gameRepo.CreateAsync(newGame);

            newGame.Id.Should().NotBeNullOrWhiteSpace("should be created by the Repository");
        }

        /// <summary>
        /// It should be possible to delete a game.
        /// </summary>
        [Fact(DisplayName = "Delete a Game")]
        public void DeleteGame()
        {
            var game = _gameGenerator.Get();

            Action act = () => _gameRepo.Delete(game);
            act.Should().NotThrow<Exception>("it should do nothing");
        }

        /// <summary>
        /// It should be possible to delete a game by its id.
        /// </summary>
        [Fact(DisplayName = "Delete a Game by Id")]
        public void DeleteGameById()
        {
            var game = _gameGenerator.Get();

            Action act = () => _gameRepo.Delete(game.Id);
            act.Should().NotThrow<Exception>("it should do nothing");
        }

        /// <summary>
        /// It should be possible to delete a game.
        /// </summary>
        [Fact(DisplayName = "Delete a Game Async")]
        public void DeleteGameAsync()
        {
            var game = _gameGenerator.Get();

            Func<Task> act = async () => await _gameRepo.DeleteAsync(game);
            act.Should().NotThrow<Exception>("it should do nothing");
        }

        /// <summary>
        /// It should be possible to delete a game by its id.
        /// </summary>
        [Fact(DisplayName = "Delete a Game by Id Async")]
        public void DeleteGameByIdAsync()
        {
            var game = _gameGenerator.Get();

            Func<Task >act = async () => await _gameRepo.DeleteAsync(game.Id);
            act.Should().NotThrow<Exception>("it should do nothing");
        }

        /// <summary>
        /// It should be possible to get many games.
        /// </summary>
        [Fact(DisplayName = "Get many Games")]
        public void GetMany()
        {
            var games = _gameRepo.Get();

            games.Should()
                .NotBeNull("should be generated by the repository").And
                .NotBeEmpty("should contain at least 2 objects");
        }

        /// <summary>
        /// It should be possible to get many games.
        /// </summary>
        [Fact(DisplayName = "Get many Games Async")]
        public async Task GetManyAsync()
        {
            var games = await _gameRepo.GetAsync();

            games.Should()
                .NotBeNull("should be generated by the repository").And
                .NotBeEmpty("should contain at least 2 objects");
        }

        /// <summary>
        /// It should be possible to get a single game.
        /// </summary>
        [Fact(DisplayName = "Get a Game")]
        public void GetSingle()
        {
            var game = _gameRepo.Get("gameId");

            game.Should().NotBeNull("should have been generated by the Repository");
        }

        /// <summary>
        /// It should be possible to get a single game.
        /// </summary>
        [Fact(DisplayName = "Get a Game async")]
        public async Task GetSingleAsync()
        {
            var game = await _gameRepo.GetAsync("gameId");

            game.Should().NotBeNull("should have been generated by the Repository");
        }

        /// <summary>
        /// It should be possible to update a game.
        /// </summary>
        [Fact(DisplayName = "Update a Game")]
        public void UpdateGame()
        {
            var game = _gameGenerator.Get();
            game.Name = "Pudim";

            Action act = () => _gameRepo.Update(game.Id, game);
            act.Should().NotThrow<Exception>("it should do nothing");
        }

        /// <summary>
        /// It should be possible to update a game.
        /// </summary>
        [Fact(DisplayName = "Update a Game Async")]
        public void UpdateGameAsync()
        {
            var game = _gameGenerator.Get();
            game.Name = "Pudim";

            Func<Task> act = async () => await _gameRepo.UpdateAsync(game.Id, game);
            act.Should().NotThrow<Exception>("it should do nothing");
        }
    }
}
