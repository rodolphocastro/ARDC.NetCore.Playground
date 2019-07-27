using ARDC.NetCore.Playground.API.ViewModels.GameViewModels;

namespace ARDC.NetCore.Playground.API.ViewModels.ReviewViewModels
{
    /// <summary>
    /// View ViewModel for the Review class.
    /// </summary>
    public class ReviewView
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the Author.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Content of the Review.
        /// </summary>
        public string ReviewText { get; set; }

        /// <summary>
        /// Final score.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The reviewed game.
        /// </summary>
        public virtual GameList ReviewSubject { get; set; }
    }
}
