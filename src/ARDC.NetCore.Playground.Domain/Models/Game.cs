using System;

namespace ARDC.NetCore.Playground.Domain.Models
{
    /// <summary>
    /// A video game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The game's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date the game was released.
        /// </summary>
        public DateTime ReleasedOn { get; set; }
    }
}
