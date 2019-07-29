using System;

namespace ARDC.NetCore.Playground.API.ViewModels.GameViewModels
{
    /// <summary>
    /// Edit ViewModel for the game Class.
    /// </summary>
    public class GameEdit
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
