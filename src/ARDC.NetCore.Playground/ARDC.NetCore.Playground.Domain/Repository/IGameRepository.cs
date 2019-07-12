using ARDC.NetCore.Playground.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.Domain.Repository
{
    /// <summary>
    /// Describe methods for accessing Games in the System.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Get a single game.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <param name="ct">Token for Task cancellation</param>
        /// <returns>The game found for the given ID</returns>
        Task<Game> GetAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Get a single game.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <returns>The game found for the given ID</returns>
        Game Get(string id);

        /// <summary>
        /// Get all the games.
        /// </summary>
        /// <param name="ct">Token for Task cancellation</param>
        /// <returns>A list of games</returns>
        Task<IList<Game>> GetAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all the games.
        /// </summary>
        /// <returns>A list of games</returns>
        IList<Game> Get();

        /// <summary>
        /// Create a new game.
        /// </summary>
        /// <param name="game">The game to be created</param>
        /// <param name="ct">Token for Task cancellation</param>
        /// <returns>The game stored in the system</returns>
        Task<Game> CreateAsync(Game game, CancellationToken ct = default);

        /// <summary>
        /// Create a new game.
        /// </summary>
        /// <param name="game">The game to be created</param>
        /// <returns>The game stored in the system</returns>
        Game Create(Game game);

        /// <summary>
        /// Update an existing game.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <param name="game">The game's updated data</param>
        /// <param name="ct">Token for Task cancellation</param>
        Task UpdateAsync(string id, Game game, CancellationToken ct = default);

        /// <summary>
        /// Update an existing game.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <param name="game">The game's updated data</param>
        void Update(string id, Game game);

        /// <summary>
        /// Delete an existing game.
        /// </summary>
        /// <param name="id">The game's id</param>
        /// <param name="ct">Token for Task cancellation</param>
        Task DeleteAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Delete an existing game.
        /// </summary>
        /// <param name="game">The game that should be deleted</param>
        /// <param name="ct">Token for Task cancellation</param>
        Task DeleteAsync(Game game, CancellationToken ct = default);

        /// <summary>
        /// Delete an existing game.
        /// </summary>
        /// <param name="id">The game's id</param>
        void Delete(string id);

        /// <summary>
        /// Delete an existing game.
        /// </summary>
        /// <param name="game">The game that should be deleted</param>
        void Delete(Game game);
    }
}
