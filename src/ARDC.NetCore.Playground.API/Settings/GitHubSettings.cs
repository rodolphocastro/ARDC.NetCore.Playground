namespace ARDC.NetCore.Playground.API.Settings
{
    /// <summary>
    /// Settings for GitHub's OAuth2.
    /// </summary>
    public class GitHubSettings
    {
        public GitHubSettings(){}

        /// <summary>
        /// Client's ID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client's Secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Callback for OAuth login.
        /// </summary>
        public string CallbackPath { get; set; }
    }
}
