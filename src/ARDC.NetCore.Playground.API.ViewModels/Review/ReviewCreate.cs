namespace ARDC.NetCore.Playground.API.ViewModels.Review
{
    /// <summary>
    /// Create ViewModel for the Review class.
    /// </summary>
    public class ReviewCreate
    {
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
        /// The reviwed game's id.
        /// </summary>
        public string SubjectId { get; set; }
    }
}
