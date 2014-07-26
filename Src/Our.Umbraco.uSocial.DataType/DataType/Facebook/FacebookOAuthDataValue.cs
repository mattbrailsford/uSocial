namespace Our.Umbraco.uSocial.DataType.Facebook
{
    /// <summary>
    /// The Data Value for the Facebook OAuth authentication data type
    /// </summary>
    public class FacebookOAuthDataValue
    {
        /// <summary>
        /// Gets or sets the user id of the user.
        /// </summary>
        /// <value>
        /// The screen name.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the OAuth token.
        /// </summary>
        /// <value>
        /// The OAuth token.
        /// </value>
        public string OAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the applications id.
        /// </summary>
        /// <value>
        /// The consumer key.
        /// </value>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the applications secret.
        /// </summary>
        /// <value>
        /// The consumer secret.
        /// </value>
		public string AppSecret { get; set; }

		/// <summary>
		/// Gets or sets the client token.
		/// </summary>
		/// <value>
		/// The client token.
		/// </value>
		public string ClientToken { get; set; }
    }
}
