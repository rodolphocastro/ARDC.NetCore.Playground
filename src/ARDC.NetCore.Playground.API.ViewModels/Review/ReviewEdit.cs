namespace ARDC.NetCore.Playground.API.ViewModels.Review
{
    /// <summary>
    /// Edit ViewModel for the Review class.
    /// </summary>
    public class ReviewEdit
    {
        /// <summary>
        /// Content of the Review.
        /// </summary>
        public string ReviewText { get; set; }

        /// <summary>
        /// Final score.
        /// </summary>
        public double Score { get; set; }
    }
}
