using System;

namespace ARDC.NetCore.Playground.API.ViewModels.GameViewModels
{
    /// <summary>
    /// View ViewModel for the game class.
    /// </summary>
    public class GameView
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
