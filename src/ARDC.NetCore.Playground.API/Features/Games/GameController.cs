using ARDC.NetCore.Playground.Domain.Models;
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
        public GameController()
        {

        }

        /// <summary>
        /// Get all the games in the system,
        /// </summary>
        /// <returns>A list of games</returns>
        [HttpGet(Name = "GET Games")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Game>))]
        public IActionResult Get() => Ok(new List<Game>());

        /// <summary>
        /// Get a specific game from the system.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <returns>The game with the specific id</returns>
        [HttpGet("{id}", Name = "GET Game")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        public IActionResult Get(string id) => Ok(new Game { Id = id });

        /// <summary>
        /// Creates a new game in the system.
        /// </summary>
        /// <param name="game">The game that should be created</param>
        /// <returns>The game created</returns>
        [HttpPost(Name = "POST Game")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Game))]
        public IActionResult Create([FromBody] Game game)
        {
            if (string.IsNullOrWhiteSpace(game.Id))
                game.Id = Guid.NewGuid().ToString();

            return Created("", game);
        }

        /// <summary>
        /// Updates an existing game in the system.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <param name="game">The updated game</param>
        [HttpPut("{id}", Name = "PUT Game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update(string id, [FromBody] Game game) { return Ok(); }

        /// <summary>
        /// Deletes an existing game from the system.
        /// </summary>
        /// <param name="id">The game's id</param>
        [HttpDelete("{id}", Name = "DELETE Game")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(string id) { return NoContent(); }
    }
}
