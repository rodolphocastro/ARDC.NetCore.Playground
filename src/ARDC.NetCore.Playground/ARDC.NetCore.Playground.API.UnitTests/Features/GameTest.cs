﻿using ARDC.NetCore.Playground.API.Features.Games;
using ARDC.NetCore.Playground.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace ARDC.NetCore.Playground.API.UnitTests.Features
{
    public class GameTest
    {
        private readonly GameController _controller;

        public GameTest()
        {
            _controller = new GameController();
        }

        /// <summary>
        /// It should be possible to get a list of games.
        /// </summary>
        [Fact(DisplayName = "Get many Games")]
        public void GetGames()
        {
            var result = _controller.Get();

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OK result")
                .Which
                .Value.Should()
                    .NotBeNull("an object is always expected, even if empty").And
                    .BeAssignableTo<IList<Game>>("it should implement IList<T>").And
                    .BeOfType<List<Game>>("it should be a List of Games");
        }

        /// <summary>
        /// It should be possible to get a single game.
        /// </summary>
        [Fact(DisplayName = "Get a Game")]
        public void GetGame()
        {
            var result = _controller.Get("gameId");

            // TODO: Futuramente o Controller poderá retornar 404 Not Found, preciso ver como testas estes casos.

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<OkObjectResult>("it should be an OK result")
                .Which
                .Value.Should()
                    .NotBeNull("a result is always expected").And
                    .BeOfType<Game>("it should be a Game")
                    .Which.Id.Should().Be("gameId");

        }

        /// <summary>
        /// It should be possible to create a game.
        /// </summary>
        [Fact(DisplayName = "Create a Game")]
        public void CreateGame()
        {
            var newGame = new Game
            {
                Name = "Pudim of War",
                ReleasedOn = DateTime.Now.AddYears(-2)
            };

            // TODO: Futuramente o controller poderá falhar em creates, rever este teste.

            var result = _controller.Create(newGame);
            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("should implement IActionResult").And
                .BeOfType<CreatedResult>("be of type Created")
                .Which
                .Value.Should()
                    .NotBeNull("it should mimic the game we sent to the controller").And
                    .BeOfType<Game>("it is created as a game")
                    .Which
                    .Id.Should()
                        .NotBeNullOrWhiteSpace("it should be generated by the Controller");

            var createdGame = (result as CreatedResult).Value;
            createdGame.Should().BeEquivalentTo(newGame, opt => opt.ExcludingMissingMembers(), "it was created using newGame itself");
        }

        /// <summary>
        /// It should be possible to update an existing game.
        /// </summary>
        [Fact(DisplayName = "Update a Game")]
        public void UpdateGame()
        {
            var game = new Game
            {
                Id = "pudimofwar",
                Name = "Pudim War",
                ReleasedOn = DateTime.Now
            };

            var result = _controller.Update(game.Id, game);
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
            var result = _controller.Delete("gameId");

            result.Should()
                .NotBeNull("a result is always expected").And
                .BeAssignableTo<IActionResult>("it should implement IActionResult").And
                .BeOfType<NoContentResult>("it should have No Content");
        }
    }
}
