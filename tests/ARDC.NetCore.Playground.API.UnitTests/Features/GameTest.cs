using ARDC.NetCore.Playground.API.Features.Games;
using ARDC.NetCore.Playground.API.ViewModels.GameViewModels;
using ARDC.NetCore.Playground.API.ViewModels.Registration;
using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using ARDC.NetCore.Playground.Persistence.Mock.Generators;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ARDC.NetCore.Playground.API.UnitTests.Features
{
    public class GameTest
    {
        private readonly IMapper _mapper;
        private readonly IModelGenerator<Game> _gameGenerator;
        private readonly Mock<IGameRepository> _mockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GameController _controller;

        public GameTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(ProfileRegistration.GetProfiles()));
            _mapper = mapperConfig.CreateMapper();
            _gameGenerator = new GameGenerator();
            _mockRepository = new Mock<IGameRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(m => m.GameRepository).Returns(_mockRepository.Object);
            _controller = new GameController(_mockUnitOfWork.Object, _mapper);
        }

        /// <summary>
        /// It should be possible to get a list of games.
        /// </summary>
        [Theory(DisplayName = "Get many Games")]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(50)]
        [InlineData(1)]
        public void GetGames(int count)
        {
            _mockRepository.Setup(m => m.Get()).Returns(_gameGenerator.Get(count));
            var result = _controller.Get();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OK result")
                .Which
                .Value.Should()
                    .NotBeNull("an object is always expected, even if empty").And
                    .BeAssignableTo<IList<GameList>>("it should implement IList<T>").And
                    .BeOfType<List<GameList>>("it should be a List of Games");

            var games = (result as OkObjectResult).Value as List<GameList>;
            games.Should()
                .NotBeEmpty("its repository has elements").And
                .HaveCount(count, $"its repository has {count} elements");
        }

        /// <summary>
        /// It should be possible to get a single game.
        /// </summary>
        [Fact(DisplayName = "Get an Existing Game")]
        public void GetGame()
        {
            var fakeGame = _gameGenerator.Get();
            _mockRepository.Setup(m => m.Get(It.IsAny<string>())).Returns(fakeGame);
            var result = _controller.Get("gameId");

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OK result")
                .Which
                .Value.Should()
                    .NotBeNull("a result is always expected").And
                    .BeOfType<GameView>("it should be a Game");

            var game = (result as OkObjectResult).Value as GameView;
            game.Should()
                .BeEquivalentTo(fakeGame, "should be the same game as the one returned by the Repository");

        }
        
        /// <summary>
        /// It should be possible to get an unexisting game without a 500 status.
        /// </summary>
        [Fact(DisplayName = "Get an Unexisting Game")]
        public void GetUnexistingGame()
        {
            _mockRepository.Setup(m => m.Get("gameId")).Returns<GameView>(null);
            var result = _controller.Get("gameId");

            result.Should()
                .NotBeNull(because: "a result is always expected").And
                .BeAssignableTo<IActionResult>(because: "it should be an implementation of IActionResult").And
                .BeOfType<NotFoundResult>(because: "it should be a 404 Not Found");
        }

        /// <summary>
        /// It should be possible to create a game.
        /// </summary>
        [Fact(DisplayName = "Create a Game")]
        public void CreateGame()
        {
            var newGame = new GameCreate
            {
                Name = "Pudim of War",
                ReleasedOn = DateTime.Now.AddYears(-2)                
            };

            _mockRepository.Setup(m => m.Create(It.IsAny<Game>())).Returns(() =>
            {
                return new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = newGame.Name,
                    ReleasedOn = newGame.ReleasedOn
                };
            });

            // TODO: Futuramente o controller poderá falhar em creates, rever este teste.

            var result = _controller.Create(newGame);
            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("should implement IActionResult").And
                .BeOfType<CreatedResult>("be of type Created")
                .Which
                .Value.Should()
                    .NotBeNull("it should mimic the game we sent to the controller").And
                    .BeOfType<GameView>("it is created as a game")
                    .Which
                    .Id.Should()
                        .NotBeNullOrWhiteSpace("it should be generated by the Repository");

            var createdGame = (result as CreatedResult).Value;
            createdGame.Should().BeEquivalentTo(newGame, opt => opt.ExcludingMissingMembers(), "it was created using newGame itself");
        }

        /// <summary>
        /// It should be possible to update an existing game.
        /// </summary>
        [Fact(DisplayName = "Update a Game")]
        public void UpdateGame()
        {
            var game = new GameEdit
            {
                Name = "Pudim War",
                ReleasedOn = DateTime.Now                
            };

            _mockRepository.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<Game>()));
            _mockRepository.Setup(m => m.Get("gameId")).Returns(new Game { Id = "gameId", Name = "Old Name", ReleasedOn = DateTime.Now.AddYears(-7) });

            var result = _controller.Update("gameId", game);
            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkResult>("it should return OK");
        }

        /// <summary>
        /// It should be possible to delete an existing game.
        /// </summary>
        [Fact(DisplayName = "Delete a Game")]
        public void DeleteGame()
        {
            _mockRepository.Setup(m => m.Delete(It.IsAny<string>()));
            _mockRepository.Setup(m => m.Delete(It.IsAny<Game>()));

            var result = _controller.Delete("gameId");

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<NoContentResult>("it should have No Content");
        }
    }
}
