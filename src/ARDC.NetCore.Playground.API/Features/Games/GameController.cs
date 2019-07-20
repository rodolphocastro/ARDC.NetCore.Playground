using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ARDC.NetCore.Playground.API.Features.Games
{
    /// <summary>
    /// Controller for Game related actions.
    /// </summary>
    [Route("games")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        /// <summary>
        /// Get all the games in the system,
        /// </summary>
        /// <returns>A list of games</returns>
        [HttpGet(Name = "GET Games")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Game>))]
        public IActionResult Get() => Ok(_gameRepository.Get());

        /// <summary>
        /// Get a specific game from the system.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <returns>The game with the specific id</returns>
        [HttpGet("{id}", Name = "GET Game")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        public IActionResult Get(string id) => Ok(_gameRepository.Get(id));

        /// <summary>
        /// Creates a new game in the system.
        /// </summary>
        /// <param name="game">The game that should be created</param>
        /// <returns>The game created</returns>
        [HttpPost(Name = "POST Game")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Game))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] Game game)
        {
            if (!string.IsNullOrWhiteSpace(game.Id))
                return BadRequest();    // TODO: Criar um teste para Games criados de maneira inválida

            var createdGame = _gameRepository.Create(game);
            return Created("", createdGame);
        }

        /// <summary>
        /// Updates an existing game in the system.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <param name="game">The updated game</param>
        [HttpPut("{id}", Name = "PUT Game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(string id, [FromBody] Game game)
        {
            try
            {
                _gameRepository.Update(id, game);
            }
            catch (Exception)   // TODO: Validar qual tipo de erro ocorreu
            {
                return BadRequest();    // TODO: Com base na exception, decidir que resposta enviar.
            }

            return Ok();
        }

        /// <summary>
        /// Deletes an existing game from the system.
        /// </summary>
        /// <param name="id">The game's id</param>
        [HttpDelete("{id}", Name = "DELETE Game")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(string id)
        {
            try
            {
                _gameRepository.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
