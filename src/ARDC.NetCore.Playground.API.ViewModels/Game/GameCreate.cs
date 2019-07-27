using System;

namespace ARDC.NetCore.Playground.API.ViewModels.Game
{
    /// <summary>
    /// Create ViewModel for the Game class.
    /// </summary>
    public class GameCreate
    {
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
