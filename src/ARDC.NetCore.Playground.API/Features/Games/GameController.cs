using ARDC.NetCore.Playground.API.ViewModels.GameViewModels;
using ARDC.NetCore.Playground.Domain;
using ARDC.NetCore.Playground.Domain.Models;
using ARDC.NetCore.Playground.Domain.Repositories;
using AutoMapper;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IGameRepository GameRepository => _unitOfWork.GameRepository;

        public GameController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all the games in the system,
        /// </summary>
        /// <returns>A list of games</returns>
        [HttpGet(Name = "GET Games")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GameList>))]
        public IActionResult Get()
        {
            var games = GameRepository.Get();
            var mappedGames = _mapper.Map<IList<GameList>>(games);
            return Ok(mappedGames);
        }

        /// <summary>
        /// Get a specific game from the system.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <returns>The game with the specific id</returns>
        [HttpGet("{id}", Name = "GET Game")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameView))]
        public IActionResult Get(string id)
        {
            var game = GameRepository.Get(id);

            if (game == null)
                return NotFound();

            var mappedGame = _mapper.Map<GameView>(game);
            return Ok(mappedGame);
        }

        /// <summary>
        /// Creates a new game in the system.
        /// </summary>
        /// <param name="game">The game that should be created</param>
        /// <returns>The game created</returns>
        [HttpPost(Name = "POST Game")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GameView))]
        public IActionResult Create([FromBody] GameCreate game)
        {
            var newGame = _mapper.Map<Game>(game);

            var createdGame = GameRepository.Create(newGame);
            _unitOfWork.SaveChanges();

            return Created("", _mapper.Map<GameView>(createdGame));
        }

        /// <summary>
        /// Updates an existing game in the system.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <param name="game">The updated game</param>
        [HttpPut("{id}", Name = "PUT Game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(string id, [FromBody] GameEdit game)
        {
            var model = GameRepository.Get(id);
            _mapper.Map(game, model);

            try
            {
                GameRepository.Update(id, model);
            }
            catch (Exception)   // TODO: Validar qual tipo de erro ocorreu
            {
                return BadRequest();    // TODO: Com base na exception, decidir que resposta enviar.
            }

            _unitOfWork.SaveChanges();

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
                GameRepository.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}
