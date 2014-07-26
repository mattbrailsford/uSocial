namespace Our.Umbraco.uSocial.DataType.Instagram
{
    /// <summary>
    /// The Data Value for the Instagram OAuth authentication data type
    /// </summary>
    public class InstagramOAuthDataValue
    {
        /// <summary>
        /// Gets or sets the screen name of the user.
        /// </summary>
        /// <value>
        /// The screen name.
        /// </value>
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets the OAuth token.
        /// </summary>
        /// <value>
        /// The OAuth token.
        /// </value>
        public string OAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the applications client id.
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the applications client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get; set; }
    }
}
