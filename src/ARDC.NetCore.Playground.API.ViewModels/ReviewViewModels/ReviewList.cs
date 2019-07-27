namespace ARDC.NetCore.Playground.API.ViewModels.ReviewViewModels
{
    /// <summary>
    /// List ViewModel for the Review class.
    /// </summary>
    public class ReviewList
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The subject's name.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Final score.
        /// </summary>
        public double Score { get; set; }
    }
}
