namespace ARDC.NetCore.Playground.Domain.Models
{
    /// <summary>
    /// A review about a Game.
    /// </summary>
    public class Review
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
        /// The game being Reviewed.
        /// </summary>
        public virtual Game Subject { get; set; }
    }
}
